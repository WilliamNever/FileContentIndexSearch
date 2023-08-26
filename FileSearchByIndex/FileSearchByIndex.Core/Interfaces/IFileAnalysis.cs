using FileSearchByIndex.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Interfaces
{
    public interface IFileAnalysis
    {
        Task<SingleFileIndexModel?> CreateSingleFileIndexAsync(string item, Action<string>? updateHandler, CancellationToken token = default);
        Task<IEnumerable<SingleFileIndexModel>> CreateFileIndexListAsync(List<string> files, Action<string>? updateHandler, CancellationToken token = default);
    }
}
