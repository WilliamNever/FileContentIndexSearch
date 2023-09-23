using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class TxtAnalysisService : TxtAnalysisBase<TxtAnalysisService>, IAnalysisService
    {
        protected ITaskHealthService _taskHealth;
        public string FileExtension => ".txt";
        protected Func<string, IAnalysisService?> _getAnalyses;
        protected override Regex WordSearchingRegex { get => CreateRegex(@"(?<word>\b[\u4e00-\u9fff]{2,}|\b([\w-]+[\s]+){2,})(.*?)(\k<word>)"); }
        public TxtAnalysisService(Func<string, IAnalysisService?> getAnalyses, ITaskHealthService taskHealth
            , IOptions<TaskThreadSettings> TaskSettings, IOptions<List<InboundFileConfig>> configs, IOptions<AppSettings> AppSettings)
            :base(AppSettings)
        {
            _taskHealth = taskHealth;
            _getAnalyses = getAnalyses;
            _taskSettings = TaskSettings.Value;
            Config = configs.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            _repeatKeywordsConfig = Config?.GetRepeatKeywordsConfig() ?? new Dictionary<int, int>();
            _minWordLength = _repeatKeywordsConfig.Count > 0 ? _repeatKeywordsConfig.Min(x => x.Key) : 0;
            InitCharEncoding(Config?.EncodingName);
        }

        public async Task<KeywordRefSampleTxtModel> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            try
            {
                var keyWords = await _taskHealth.RunHealthTaskWithAutoRestWaitAysnc(async (tk) =>
                {
                    KeywordRefSampleTxtModel kws;

                    if (Config?.CanAutoSelectAnalysisService ?? false)
                    {
                        IAnalysisService analysis =
                            _getAnalyses(HasChineseInFileName(Path.GetFileName(file)) ? ".txt.cn" : ".txt.en")
                            ?? throw new Exception($"No analysis for file {file}");
                        kws = await analysis.AnalysisFileKeyWorks(file, updateHandler, tk);
                    }
                    else
                    {
                        kws = await AnalysisInCommonAsync(file, updateHandler, tk);
                    }
                    return kws;
                }, token);

                keyWords.KeyWords = keyWords.KeyWords.OrderBy(x => x.Frequency).ToList();
                keyWords.SampleTxts = keyWords.SampleTxts.OrderBy(x => x.LineNumber).ToList();
                return keyWords;
            }
            catch (OperationCanceledException ex)
            {
                ex.Data.Add("Source file", $"Failed to Analysis {file} for time out!");
                updateHandler?.Invoke($"Failed to Analysis {file} for time out!");
                throw;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Source file", $"Failed to Analysis {file}");
                throw;
            }
        }

        private async Task<KeywordRefSampleTxtModel> AnalysisInCommonAsync(string file, Action<string>? updateHandler, CancellationToken token)
        {
            KeywordRefSampleTxtModel keywordRef = new();

            var txt = await ReadFileAsync(file);
            IEnumerable<string> strKWs = await PickupkeywordsAsync(txt, token);

            updateHandler?.Invoke($"{strKWs.LongCount()} keywords for {file} -");
            if (strKWs != null && strKWs.Any())
                await Parallel.ForEachAsync(strKWs, new ParallelOptions { MaxDegreeOfParallelism = _taskSettings.TaskInitCount, CancellationToken = token },
                    async (item, token) =>
                        await Task.Run(async () =>
                        {
                            if (token.IsCancellationRequested)
                                throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                            KeyWordsModel? kwordModel = await CreateKeywordModelAsync(txt, item, token);
                            if (!string.IsNullOrEmpty(kwordModel?.KeyWord))
                            {
                                kwordModel.LineNumbers = kwordModel.SampleTxts.Select(x => x.LineNumber).OrderBy(x => x).ToList();
                                lock (LockObj)
                                {
                                    AddKeywordToKeyRef(keywordRef, kwordModel);
                                }
                            }
                        }
                    , token));
            return keywordRef;
        }

        private async Task<KeyWordsModel?> CreateKeywordModelAsync(string txt, string item, CancellationToken token)
        {
            KeyWordsModel rsl = new() { KeyWord = item, KeyWordsType = Core.Enums.EnKeyWordsType.FlatText };
            Regex regex = CreateRegex($"((\r)?{EnviConst.SpecNewLine1})(.+({item})+.+)+?\\1");
            var Txt_AddFirstLine = $"{EnviConst.EnvironmentNewLine}{txt}{EnviConst.EnvironmentNewLine}";

            var matches = regex.Matches(Txt_AddFirstLine);
            if ((item?.Length ?? -1) < _minWordLength)
            {
                return null;
            }
            else
            {
                if (_repeatKeywordsConfig.TryGetValue(item!.Length, out int requiredShows) && matches.Count < requiredShows)
                    return null;
            }

            List<SampleTxtModel?> rtvs = await RunParallelForEach(matches.OfType<Match>(),
                async (match, tk) =>
                {
                    return await Task.FromResult(new SampleTxtModel
                    {
                        LineNumber = GetCurrentLineNumber(Txt_AddFirstLine, match.Value.Trim(), match),
                        Text = match.Value.Trim()
                    });
                }
                , _taskSettings.TaskInitCount, token);
            rsl.SampleTxts.AddRange(rtvs.Where(x => x != null)!);
            rsl.SampleTxts = rsl.SampleTxts.Distinct(new SampleTxtModel()).ToList();
            return await Task.FromResult(rsl);
        }

        private async Task<IEnumerable<string>> PickupkeywordsAsync(string txt, CancellationToken token)
        {
            var matches = WordSearchingRegex.Matches(txt).OfType<Match>().ToList();
            var srchMatches = matches.Where(m => m.Length > (Config?.SmallCharacterNumberInString ?? 50));

            List<Match> tmpMatches;
            while (srchMatches.Any())
            {
                tmpMatches = new List<Match>();
                foreach (var match in srchMatches)
                {
                    if (token.IsCancellationRequested)
                        throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                    tmpMatches.AddRange(WordSearchingRegex.Matches(LineWrap.Replace(match.Value ?? "", " "), match.Groups["word"].Length)
                        .Select(x => x).OfType<Match>());
                }

                matches.AddRange(tmpMatches);
                srchMatches = tmpMatches.Where(m => m.Length > (Config?.SmallCharacterNumberInString ?? 50));
            }

            return await Task.FromResult(matches.Select(x => x.Groups["word"].Value.Trim()).Distinct());
        }
    }
}