using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class TxtAnalysisService : TxtAnalysisBase<TxtAnalysisService>, IAnalysisService
    {
        public string FileExtension => ".txt";

        protected InboundFileConfig? Config;
        protected Func<string, IAnalysisService?> _getAnalyses;
        protected TaskThreadSettings _taskSettings;

        protected override Regex WordSearchingRegex => new(@"(?<word>\b[\u4e00-\u9fa5\d_]{2,}|\b[\w -]{5,})(.*?)(\k<word>)");
        //protected override Regex WordSearchingRegex => new(@"(?<word>\b[\u4e00-\u9fa5\d_]{2,})(.*?)(\k<word>)");
        //protected override Regex WordSearchingRegex => new(@"(?<word>\b[\w -]{5,})(.*?)(\k<word>)");
        public TxtAnalysisService(Func<string, IAnalysisService?> getAnalyses, IOptions<TaskThreadSettings> TaskSettings, IOptions<List<InboundFileConfig>> configs)
        {
            _getAnalyses = getAnalyses;
            _taskSettings = TaskSettings.Value;
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
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
            return keyWords;
        }

        private async Task<IEnumerable<KeyWordsModel>> AnalysisInCommonAsync(string file, Action<string>? updateHandler, CancellationToken token)
        {
            List<KeyWordsModel> keyWords = new();
            try
            {
                var txt = await ReadFileAsync(file);
                var matches = WordSearchingRegex.Matches(txt).OfType<Match>().ToList();
                var matches1 = WordSearchingRegex.Matches(txt, matches[10].Index + 1).OfType<Match>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return keyWords;
        }
    }
}