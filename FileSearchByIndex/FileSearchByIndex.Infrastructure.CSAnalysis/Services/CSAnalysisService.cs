using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Infrastructure.CSAnalysis.Services
{
    public class CSAnalysisService : BaseAnalysis<CSAnalysisService>, IAnalysisService
    {
        protected InboundFileConfig? Config;
        protected TaskThreadSettings _taskSettings;
        public CSAnalysisService(IOptions<TaskThreadSettings> TaskSettings, IOptions<List<InboundFileConfig>> configs)
        {
            _taskSettings = TaskSettings.Value;
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            if (Config?.EncodingName != null) InitCharEncoding(Config.EncodingName);
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileExtension { get => ".cs"; }
        protected Regex regString = new Regex($"\\\"[\\w\\W]*?\\\"");
        protected Regex regDCom = new Regex($"\\\"");

        protected virtual Regex PickerOfCommentKeyWords1 { get => new($"((^[\\s]*)|([\\s]+))/// <summary>[\\w\\W]+?/// </summary>"); }
        protected virtual Regex PickerOfCommentKeyWords2 { get => new($"((^[\\s]*)|([\\s]+))/\\*[\\w\\W]+?\\*/"); }
        protected virtual Regex PickerClassName { get => new($"([\\w\\s]*(class|interface|enum|struct){{1}})[ ]+[\\w]+[\\w\\W]*?({EnviConst.EnvironmentNewLine}|{EnviConst.SpecNewLine1})"); }
        //protected virtual Regex PickerMethodsName { get => new($"({EnviConst.EnvironmentNewLine}|{EnviConst.SpecNewLine1})[\\s]+(private|public|protected|internal){{1}}[\\s]+[\\w. <>]+(\\(([\\w.<>\\?: ]+[ ]+[\\w= ]+[,]?)*\\)){{1}}[\\s\\w:]*"); }
        protected virtual Regex PickerMethodsName { get => new($"({EnviConst.EnvironmentNewLine}|{EnviConst.SpecNewLine1})[\\s]+((private|public|protected|internal)[\\s]+)[\\w. <>?\\[\\]]+\\([\\w <>.\\?:=,\"\']*\\)([\\s]+where[\\w\\s:.<>\\[\\]]+)*"); }
        //protected virtual Regex PickerMethodsName { get => new($"({EnviConst.EnvironmentNewLine}|{EnviConst.SpecNewLine1})[\\s]+((private|public|protected|internal)[\\s]+)?[\\w. <>?\\[\\]]+\\([\\w <>.\\?:=,\"\']*\\)([\\s]+where[\\w :.]+)*"); }
        //protected virtual Regex PickerMethodsName { get => new($"({EnviConst.EnvironmentNewLine}|{EnviConst.SpecNewLine1})[\\s]+(private|public|protected|internal)[\\s]+[\\w. <>?]+\\([\\w <>.\\?:=,\"\']*\\)([\\s]+where[\\w :.]+)*"); }
        protected virtual Regex PickerPropertiesName { get => new($"({EnviConst.EnvironmentNewLine}|{EnviConst.SpecNewLine1})[\\s]+(private|public|protected|internal){{1}}[\\s]+[\\w. <>?\\[\\]]+{{"); }
        protected virtual Regex PickerCommandInUsing { get => new($"({EnviConst.EnvironmentNewLine}|{EnviConst.SpecNewLine1})[\\s\\w\\(\\)\\[\\].=<>]+[\\w<>]\\([\\w\\W]*?\\)"); }

        public async Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            List<KeyWordsModel> keyWords = new List<KeyWordsModel>();
            try
            {
                var txt = await ReadFileAsync(file);
                var rsl = await Task.WhenAll(
                    GetCommentKeyWordsAsync(file, txt, updateHandler, token),
                    GetNameKeyWordsAsync(file, txt, updateHandler, token),
                    GetCommandKeyWordsAsync(file, txt, updateHandler, token));

                foreach (var keyWord in rsl)
                    if (keyWord != null) keyWords.AddRange(keyWord);

            }
            catch (Exception)
            {
                throw;
            }
            return keyWords;
        }
        /// <summary>
        /// pick up the keywords in commands
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="updateHandler"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<IEnumerable<KeyWordsModel>> GetCommandKeyWordsAsync(string file, string txt, Action<string>? updateHandler, CancellationToken token)
        {
            /*
             * pick up the commands in use
             */
            Regex regCommandName = new Regex($"[\\w.<>]+[\\s]*\\(");
            List<KeyWordsModel> keyWords = new List<KeyWordsModel>();
            var matches = PickerCommandInUsing.Matches(txt).ToList();
            try
            {
                var keysTxtList = matches.Select(x => new SampleTxtModel { LineNumber = GetCurrentLineNumber(txt, x.Value.Trim(), x) + 1, Text = x.Value.Trim() }).ToList();
                var mCmds = keysTxtList.SelectMany(kt => regCommandName.Matches(ClearString(kt?.Text?.Trim()??"", "\"")).Select(x => x.Value.Trim().TrimEnd('('))).Distinct().ToList();
                #region comment out code
                //foreach (var mc in mCmds)
                //{
                //    var list = keysTxtList.Where(x => x.Text.Contains(mc));
                //    if (list.Any())
                //    {
                //        keyWords.Add(new KeyWordsModel
                //        {
                //            KeyWord = mc,
                //            KeyWordsType = Core.Enums.EnKeyWordsType.CommandName,
                //            SampleTxts = list.ToList()
                //        });
                //    }
                //}
                #endregion

                var compare = SampleTxtModel.GetComparer();
                if (mCmds.Any())
                    await Parallel.ForEachAsync(mCmds, new ParallelOptions { MaxDegreeOfParallelism = _taskSettings.TaskInitCount },
                        async (item, token) =>
                            await Task.Run(() =>
                            {
                                try
                                {
                                    if (token.IsCancellationRequested)
                                    {
                                        return;
                                    }
                                    var list = keysTxtList.Where(x => x.Text.Contains(item)).Select(x => x).Distinct(compare);
                                    if (list.Any())
                                    {
                                        lock (keyWords)
                                        {
                                            keyWords.Add(new KeyWordsModel
                                            {
                                                KeyWord = item,
                                                KeyWordsType = Core.Enums.EnKeyWordsType.CommandName,
                                                SampleTxts = list.ToList()
                                            });
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _log.Error($"{item} broke - {EnviConst.EnvironmentNewLine}", ex);
                                    updateHandler?.Invoke($"{item} broke in searching file {file} type is {Core.Enums.EnKeyWordsType.CommandName.ToString()}" +
                                        $" - {ex.Message} - {EnviConst.EnvironmentNewLine}");
                                }
                                finally
                                {
                                }
                            }
                        , token));

            }
            catch (Exception ex)
            {
                _log.Error($"Failed to pick up command keyword from {file}", ex);
            }

            return keyWords;
        }

        /// <summary>
        /// Pick up keyworkds in class, method names
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="updateHandler"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<IEnumerable<KeyWordsModel>> GetNameKeyWordsAsync(string file, string txt, Action<string>? updateHandler, CancellationToken token)
        {
            /*
             * Pick up the class, method and properties names
             */
            List<KeyWordsModel> keyWords = new();
            var matches = PickerClassName.Matches(txt).ToList();
            matches.AddRange(PickerMethodsName.Matches(txt));
            matches.AddRange(PickerPropertiesName.Matches(txt));
            try
            {
                foreach (var m in matches)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    keyWords.Add(CreateKeyword(txt, m, Core.Enums.EnKeyWordsType.MethodOrClassName));
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Failed to pick up the class, method and properties names from {file}", ex);
            }

            return keyWords;
        }

        /// <summary>
        /// Pick up keyworkds in Comments
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="updateHandler"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<IEnumerable<KeyWordsModel>> GetCommentKeyWordsAsync(string file, string txt, Action<string>? updateHandler, CancellationToken token)
        {
            /*
             * 1- 
                    /// <summary>
                    /// 
                    /// </summary>
                2-
                    /*  *_/
             */
            List<KeyWordsModel> keyWords = new();
            var matches = PickerOfCommentKeyWords1.Matches(txt).ToList();
            matches.AddRange(PickerOfCommentKeyWords2.Matches(txt));
            try
            {
                foreach (var m in matches)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    keyWords.Add(CreateKeyword(txt, m, Core.Enums.EnKeyWordsType.Comment));
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Failed to pick up keywords in Comment from {file}", ex);
            }

            return keyWords;
        }

        private KeyWordsModel CreateKeyword(string txt, Match m, Core.Enums.EnKeyWordsType kvType)
        {
            var kv = new KeyWordsModel
            {
                KeyWord = EmptyChars.Replace(LineWrap.Replace(m.Value, ""), " ").Trim().TrimEnd('{'),
                KeyWordsType = kvType,
            };
            kv.SampleTxts.Add(new SampleTxtModel { LineNumber = GetCurrentLineNumber(txt, m.Value.Trim(), m) + 1, Text = m.Value.Trim().TrimEnd('{') });
            return kv;
        }
        private int GetCurrentLineNumber(string txt, string partTxt, Match match)
        {
            return LineWrap.Matches(txt[0..txt.IndexOf(partTxt?.Trim() ?? "", match.Index)]).Count;
        }
    }
}
