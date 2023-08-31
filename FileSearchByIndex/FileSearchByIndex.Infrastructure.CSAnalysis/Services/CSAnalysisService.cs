using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Infrastructure.CSAnalysis.Services
{
    public class CSAnalysisService : BaseAnalysis<CSAnalysisService>, IAnalysisService
    {
        protected InboundFileConfig? Config;
        public CSAnalysisService(IOptions<List<InboundFileConfig>> configs)
        {
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            if (Config != null) InitCharEncoding(Config.EncodingName);
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileExtension { get => ".cs"; }
        protected virtual Regex PickerOfCommentKeyWords1 { get => new($"((^[\\s]*)|([\\s]+))/// <summary>[\\w\\W]+?/// </summary>"); }
        protected virtual Regex PickerOfCommentKeyWords2 { get => new($"((^[\\s]*)|([\\s]+))/\\*[\\w\\W]+?\\*/"); }
        protected virtual Regex PickerClassName { get => new($"([\\w\\s]*(class|interface|enum|struct){{1}})[ ]+[\\w]+[\\w\\W]*?({EnviConst.NewLine})"); }
        protected virtual Regex PickerMethodsName { get => new($"({EnviConst.NewLine})[\\s]+(private|public|protected|internal){{1}}[\\s]+[\\w. <>]+(\\(([\\w.<>\\?: ]+[ ]+[\\w= ]+[,]?)*\\)){{1}}[\\s\\w:]*"); }
        protected virtual Regex PickerPropertiesName { get => new($"({EnviConst.NewLine})[\\s]+(private|public|protected|internal){{1}}[\\s]+[\\w. <>]+{{"); }
        protected virtual Regex PickerCommandInUsing { get => new($"({EnviConst.NewLine})[\\s\\w\\(\\).=<>]+[\\w<>]\\([\\w\\W]*?\\)"); }

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
            List<KeyWordsModel> keyWords = new List<KeyWordsModel>();
            var matches = PickerCommandInUsing.Matches(txt).ToList();
            try
            {
                var keysTxtList = matches.Select(x => x.Value.Trim()).ToList();
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
            List<KeyWordsModel> keyWords = new List<KeyWordsModel>();
            var matches = PickerClassName.Matches(txt).ToList();
            matches.AddRange(PickerMethodsName.Matches(txt));
            matches.AddRange(PickerPropertiesName.Matches(txt));
            try
            {
                foreach (var m in matches)
                    keyWords.Add(CreateKeyword(m, Core.Enums.EnKeyWordsType.MethodOrClassName));
                
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
            List<KeyWordsModel> keyWords = new List<KeyWordsModel>();
            var matches = PickerOfCommentKeyWords1.Matches(txt).ToList();
            matches.AddRange(PickerOfCommentKeyWords2.Matches(txt));
            try
            {
                foreach (var m in matches)
                    keyWords.Add(CreateKeyword(m, Core.Enums.EnKeyWordsType.Comment));
            }
            catch (Exception ex)
            {
                _log.Error($"Failed to pick up keywords in Comment from {file}", ex);
            }

            return keyWords;
        }

        private KeyWordsModel CreateKeyword(Match m, Core.Enums.EnKeyWordsType kvType)
        {
            var kv = new KeyWordsModel
            {
                KeyWord = EmptyChars.Replace(LineWrap.Replace(m.Value, ""), " ").Trim(),
                KeyWordsType = kvType,
            };
            kv.SampleTxts.Add(new SampleTxtModel { LineNumber = GetCurrentLineNumber(m.Value.Trim()) + 1, Text = m.Value.Trim() });
            return kv;
        }
        private int GetCurrentLineNumber(string txt)
        {
            return LineWrap.Matches(txt[0..txt.IndexOf(txt?.Trim() ?? "")]).Count;
        }

        private async Task<string> ReadFileAsync(string path)
        {
            try
            {
                using (StreamReader sr = new(path, FileEncoding, true
                    , new FileStreamOptions() { Mode = FileMode.Open, Access = FileAccess.Read, Share = FileShare.ReadWrite }
                    ))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Error(s) in Reading file {path}", ex);
                throw;
            }
        }
    }
}
