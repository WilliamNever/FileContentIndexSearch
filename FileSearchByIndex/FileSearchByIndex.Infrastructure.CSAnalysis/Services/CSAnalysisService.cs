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
        protected ITaskHealthService _taskHealth;
        public CSAnalysisService(ITaskHealthService taskHealth, IOptions<TaskThreadSettings> TaskSettings, 
            IOptions<List<InboundFileConfig>> configs, IOptions<AppSettings> AppSettings)
            :base(AppSettings)
        {
            _taskHealth = taskHealth;
            _taskSettings = TaskSettings.Value;
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            InitCharEncoding(Config?.EncodingName);
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileExtension { get => ".cs"; }
        protected Regex regString { get => CreateRegex($"\\\"[\\w\\W]*?\\\""); }
        protected Regex regDCom { get => CreateRegex($"\\\""); }

        protected virtual Regex PickerOfCommentKeyWords1 { get => CreateRegex($"((^[\\s]*)|([\\s]+))/// <summary>[\\w\\W]+?/// </summary>"); }
        protected virtual Regex PickerOfCommentKeyWords2 { get => CreateRegex($"((^[\\s]*)|([\\s]+))/\\*[\\w\\W]+?\\*/"); }
        protected virtual Regex PickerClassName { get => CreateRegex($"([\\w\\s]*(class|interface|enum|struct){{1}})[ ]+[\\w]+[\\w\\W]*?((\r)?{EnviConst.SpecNewLine1})"); }
        protected virtual Regex PickerMethodsName { get => CreateRegex($"((\r)?{EnviConst.SpecNewLine1})[\\s]+((private|public|protected|internal)[\\s]+)[\\w. <>?\\[\\]]+\\([\\w <>.\\?:=,\"\']*\\)([\\s]+where[\\w\\s:.<>\\[\\]]+)*"); }
        protected virtual Regex PickerPropertiesName { get => CreateRegex($"((\r)?{EnviConst.SpecNewLine1})[\\s]+(private|public|protected|internal){{1}}[\\s]+[\\w. <>?\\[\\]]+{{"); }
        protected virtual Regex PickerCommandInUsing { get => CreateRegex($"((\r)?{EnviConst.SpecNewLine1})[\\s\\w\\(\\)\\[\\].=<>]+[\\w<>]\\([\\w\\W]*?\\)"); }

        public async Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            try
            {
                var keyWords = await _taskHealth.RunHealthTaskWithAutoRestWaitAysnc(async tk =>
                {
                    List<KeyWordsModel> kws = new List<KeyWordsModel>();

                    var txt = await ReadFileAsync(file);
                    var rsl = await Task.WhenAll(
                    GetCommentKeyWordsAsync(file, txt, updateHandler, tk),
                    GetNameKeyWordsAsync(file, txt, updateHandler, tk),
                    GetCommandKeyWordsAsync(file, txt, updateHandler, tk));

                    foreach (var keyWord in rsl)
                    {
                        if (tk.IsCancellationRequested)
                            throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                        if (keyWord != null)
                        {
                            var list = keyWord.ToList();
                            list.ForEach(x => x.LineNumbers = x.SampleTxts.Select(y => y.LineNumber).ToList());
                            kws.AddRange(list);
                        }
                    }
                    return kws;
                }, token);

                return keyWords.OrderBy(x => x.Frequency);
            }
            catch (OperationCanceledException ex)
            {
                ex.Data.Add("Source file", $"Failed to Analysis {file} for time out!");
                _log.Error($"Failed to Analysis for time out!", ex);
                throw;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Source file", $"Failed to Analysis {file} for time out!");
                _log.Error($"Failed to pick up command keyword from {file}", ex);
                throw;
            }
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
            Regex regCommandName = CreateRegex($"[\\w.<>]+[\\s]*\\(");
            List<KeyWordsModel> keyWords = new List<KeyWordsModel>();
            var matches = PickerCommandInUsing.Matches(txt).ToList();
            var keysTxtList = matches.Select(x => new SampleTxtModel
            {
                LineNumber =
                token.IsCancellationRequested ?
                throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}") :
                GetCurrentLineNumber(txt, x.Value.Trim(), x) + 1,
                Text = x.Value.Trim()
            }).ToList();

            var mCmds = keysTxtList.SelectMany(kt =>
            token.IsCancellationRequested ?
                throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}") :
                regCommandName.Matches(ClearString(kt?.Text?.Trim() ?? "", "\"")).Select(x => x.Value.Trim().TrimEnd('('))).Distinct().ToList();

            var compare = SampleTxtModel.GetComparer();
            if (mCmds.Any())
                await Parallel.ForEachAsync(mCmds, new ParallelOptions { MaxDegreeOfParallelism = _taskSettings.TaskInitCount, CancellationToken = token },
                    async (item, token) =>
                        await Task.Run(() =>
                        {
                            if (token.IsCancellationRequested)
                                throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
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
                    , token));
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
            foreach (var m in matches)
            {
                if (token.IsCancellationRequested)
                    throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                keyWords.Add(CreateKeyword(txt, m, Core.Enums.EnKeyWordsType.MethodOrClassName, '{'));
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
            foreach (var m in matches)
            {
                if (token.IsCancellationRequested)
                    throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                keyWords.Add(CreateKeyword(txt, m, Core.Enums.EnKeyWordsType.Comment, '{'));
            }
            return keyWords;
        }
    }
}
