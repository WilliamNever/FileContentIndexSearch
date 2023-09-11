using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class TxtAnalysisEntrance : BaseAnalysis<TxtAnalysisEntrance>, IAnalysisService
    {
        public string FileExtension => ".txt";

        protected InboundFileConfig? Config;
        protected Func<string, IAnalysisService?> _getAnalyses;

        protected virtual Regex WordSearchingRegex { get => new($"[\u4e00-\u9fa5]|([\\w]+)"); }
        public TxtAnalysisEntrance(Func<string, IAnalysisService?> getAnalyses, IOptions<List<InboundFileConfig>> configs)
        {
            _getAnalyses = getAnalyses;
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            if (Config?.EncodingName != null) InitCharEncoding(Config.EncodingName!);
        }

        public async Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            List<KeyWordsModel> keyWords = new List<KeyWordsModel>();
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
