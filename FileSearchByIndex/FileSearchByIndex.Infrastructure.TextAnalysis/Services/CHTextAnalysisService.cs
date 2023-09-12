using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class CHTextAnalysisService : TxtAnalysisBase<CHTextAnalysisService>, IAnalysisService
    {
        public string FileExtension => ".txt.cn";

        protected InboundFileConfig? Config;
        protected TaskThreadSettings _taskSettings;
        protected override Regex WordSearchingRegex => new(@"(?<word>\b[\u4e00-\u9fa5\d_]{2,})(.*?)(\k<word>)");
        public CHTextAnalysisService(IOptions<TaskThreadSettings> TaskSettings, IOptions<List<InboundFileConfig>> configs)
        {
            _taskSettings = TaskSettings.Value;
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
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
