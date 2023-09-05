using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class CHTextAnalysisService : BaseAnalysis<CHTextAnalysisService>, IAnalysisService
    {
        public string FileExtension => ".txt.cn";

        public Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
