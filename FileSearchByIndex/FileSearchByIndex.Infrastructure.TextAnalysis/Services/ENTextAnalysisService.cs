using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class ENTextAnalysisService : BaseAnalysis<ENTextAnalysisService>, IAnalysisService
    {
        public string FileExtension => ".txt.en";

        protected InboundFileConfig? Config;
        protected TaskThreadSettings _taskSettings;
        public ENTextAnalysisService(IOptions<TaskThreadSettings> TaskSettings, IOptions<List<InboundFileConfig>> configs)
        {
            _taskSettings = TaskSettings.Value;
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            if (Config != null) InitCharEncoding(Config.EncodingName);
        }
        public Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
