using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class CHTextAnalysisService : BaseAnalysis<CHTextAnalysisService>, IAnalysisService
    {
        public string FileExtension => ".txt.cn";

        protected InboundFileConfig? Config;
        protected TaskThreadSettings _taskSettings;
        public CHTextAnalysisService(IOptions<TaskThreadSettings> TaskSettings, IOptions<List<InboundFileConfig>> configs)
        {
            _taskSettings = TaskSettings.Value;
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            if (Config?.EncodingName != null) InitCharEncoding(Config.EncodingName!);
        }

        public Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
