﻿using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class TxtAnalysisService : TxtAnalysisBase<TxtAnalysisService>, IAnalysisService
    {
        public string FileExtension => ".txt";
        protected Func<string, IAnalysisService?> _getAnalyses;
        //protected override Regex WordSearchingRegex => new(@"(?<word>\b[\u4e00-\u9fa5\d_]{2,}|\b[\w -]{5,})(.*?)(\k<word>)");
        //protected override Regex WordSearchingRegex => new(@"(?<word>\b[\u3400-\u4dbf\u4e00-\u9fff\uf900-\ufaff\u20000-\u3134a\d_-]{2,}|\b[\w -]{5,})(.*?)(\k<word>)");
        //protected override Regex WordSearchingRegex => new(@"(?<word>\b[\u4e00-\u9fff\uff00-\uffee\u20000-\u3134a\d_-]{2,}|\b[\w -]{5,})(.*?)(\k<word>)");
        protected override Regex WordSearchingRegex => new(@"(?<word>\b[\u4e00-\u9fff]{2,}|\b([\w-]+[\s]+){2,})(.*?)(\k<word>)");
        public TxtAnalysisService(Func<string, IAnalysisService?> getAnalyses, IOptions<TaskThreadSettings> TaskSettings, IOptions<List<InboundFileConfig>> configs)
        {
            _getAnalyses = getAnalyses;
            _taskSettings = TaskSettings.Value;
            Config = configs.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            _repeatKeywordsConfig = Config?.GetRepeatKeywordsConfig() ?? new Dictionary<int, int>();
            _minWordLength = _repeatKeywordsConfig.Count > 0 ? _repeatKeywordsConfig.Min(x => x.Key) : 0;
            InitCharEncoding(Config?.EncodingName);
        }

        public async Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            List<KeyWordsModel> keyWords = new();

            if (Config?.CanAutoSelectAnalysisService ?? false)
            {
                IAnalysisService analysis =
                    _getAnalyses(HasChineseInFileName(Path.GetFileName(file)) ? ".txt.cn" : ".txt.en")
                    ?? throw new Exception($"No analysis for file {file}");
                keyWords.AddRange(await analysis.AnalysisFileKeyWorks(file, updateHandler, token));
            }
            else
            {
                keyWords.AddRange(await AnalysisInCommonAsync(file, updateHandler, token));
            }
            keyWords.ForEach(x => x.LineNumbers = x.SampleTxts.Select(y => y.LineNumber).ToList());
            return keyWords.OrderBy(x => x.Frequency);
        }

        private async Task<IEnumerable<KeyWordsModel>> AnalysisInCommonAsync(string file, Action<string>? updateHandler, CancellationToken token)
        {
            List<KeyWordsModel> keyWords = new();
            try
            {
                var txt = await ReadFileAsync(file);
                var strKWs = await PickupkeywordsAsync(txt);

                if (strKWs != null && strKWs.Any())
                    await Parallel.ForEachAsync(strKWs, new ParallelOptions { MaxDegreeOfParallelism = _taskSettings.TaskInitCount },
                        async (item, token) =>
                            await Task.Run(async () =>
                            {
                                try
                                {
                                    if (token.IsCancellationRequested)
                                    {
                                        return;
                                    }
                                    var kwordModel = await CreateKeywordModelAsync(txt, item, token);
                                    if (kwordModel != null)
                                        lock (keyWords)
                                        {
                                            keyWords.Add(kwordModel);
                                        }
                                }
                                catch (Exception ex)
                                {
                                    _log.Error($"{item} broke - {EnviConst.EnvironmentNewLine}", ex);
                                    updateHandler?.Invoke($"{item} broke - {ex.Message} - {EnviConst.EnvironmentNewLine}");
                                }
                                finally
                                {
                                }
                            }
                        , token));

            }
            catch (Exception)
            {
                throw;
            }
            return keyWords;
        }

        private async Task<KeyWordsModel?> CreateKeywordModelAsync(string txt, string item, CancellationToken token)
        {
            KeyWordsModel rsl = new() { KeyWord = item, KeyWordsType = Core.Enums.EnKeyWordsType.FlatText };
            Regex regex = new Regex($"((\\r)?{EnviConst.SpecNewLine1})(.+({item})+.+)+?\\1");
            var matches = regex.Matches(txt);

            if ((item?.Length ?? -1) < _minWordLength)
            {
                return null;
            }
            else
            {
                if (_repeatKeywordsConfig.TryGetValue(item!.Length, out int requiredShows) && matches.Count < requiredShows)
                    return null;
            }

            #region
            //foreach (var match in matches.OfType<Match>())
            //{
            //    rsl.SampleTxts.Add(new SampleTxtModel
            //    {
            //        LineNumber = GetCurrentLineNumber(txt, match.Value.Trim(), match),
            //        Text = match.Value.Trim()
            //    });
            //}
            #endregion

            List<SampleTxtModel?> rtvs = await RunParallelForEach(matches.OfType<Match>(),
                async match => {
                    return await Task.FromResult(new SampleTxtModel
                    {
                        LineNumber = GetCurrentLineNumber(txt, match.Value.Trim(), match),
                        Text = match.Value.Trim()
                    });
                }
                , _taskSettings.TaskInitCount, token);
            rsl.SampleTxts.AddRange(rtvs.Where(x => x != null)!);
            rsl.SampleTxts = rsl.SampleTxts.Distinct(new SampleTxtModel()).ToList();
            return await Task.FromResult(rsl);
        }

        private async Task<IEnumerable<string>> PickupkeywordsAsync(string txt)
        {
            var matches = WordSearchingRegex.Matches(txt).OfType<Match>().ToList();
            var srchMatches = matches.Where(m => m.Length > (Config?.SmallCharacterNumberInString ?? 50));

            List<Match> tmpMatches;
            while (srchMatches.Any())
            {
                tmpMatches = new List<Match>();
                foreach (var match in srchMatches)
                    tmpMatches.AddRange(WordSearchingRegex.Matches(LineWrap.Replace(match.Value ?? "", " "), match.Groups["word"].Length).OfType<Match>());

                matches.AddRange(tmpMatches);
                srchMatches = tmpMatches.Where(m => m.Length > (Config?.SmallCharacterNumberInString ?? 50));
            }

            return await Task.FromResult(matches.Select(x => x.Groups["word"].Value.Trim()).Distinct());
        }
    }
}