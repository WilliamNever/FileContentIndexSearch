using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using Microsoft.Extensions.Options;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class TxtAnalysisEntrance : BaseAnalysis<TxtAnalysisEntrance>, IAnalysisService
    {
        protected InboundFileConfig? Config;
        public string FileExtension => ".txt";
        protected Func<string, IAnalysisService?> _getAnalyses;
        public TxtAnalysisEntrance(Func<string, IAnalysisService?> getAnalyses, IOptions<List<InboundFileConfig>> configs)
        {
            _getAnalyses = getAnalyses;
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            if (Config != null) InitCharEncoding(Config.EncodingName);
        }

        public Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
