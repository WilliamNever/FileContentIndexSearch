using FileSearchByIndex.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Interfaces
{
    public interface IAnalysisService
    {
        public string FileExtension { get; }

        Task<KeywordRefSampleTxtModel> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default);
    }
}
