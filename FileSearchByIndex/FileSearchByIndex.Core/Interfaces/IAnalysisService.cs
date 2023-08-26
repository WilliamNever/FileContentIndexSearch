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

        Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default);
    }
}
