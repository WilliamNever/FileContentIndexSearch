using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
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

        public Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
