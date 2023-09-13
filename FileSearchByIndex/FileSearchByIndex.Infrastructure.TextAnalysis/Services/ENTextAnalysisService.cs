using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class ENTextAnalysisService : TxtAnalysisBase<ENTextAnalysisService>, IAnalysisService
    {
        public string FileExtension => ".txt.en";
        protected override Regex WordSearchingRegex => new(@"(?<word>\b[\w -]{5,})(.*?)(\k<word>)");
        public ENTextAnalysisService(IOptions<TaskThreadSettings> TaskSettings, IOptions<List<InboundFileConfig>> configs)
        {
            _taskSettings = TaskSettings.Value;
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            _repeatKeywordsConfig = Config?.GetRepeatKeywordsConfig() ?? new Dictionary<int, int>();
            _minWordLength = _repeatKeywordsConfig.Count > 0 ? _repeatKeywordsConfig.Min(x => x.Key) : 0;
            InitCharEncoding(Config?.EncodingName);
        }
        public async Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            List<KeyWordsModel> keyWords = new();
            try
            {
                var txt = await ReadFileAsync(file);
            }
            catch (Exception)
            {
                throw;
            }
            return keyWords;
        }
    }
}
