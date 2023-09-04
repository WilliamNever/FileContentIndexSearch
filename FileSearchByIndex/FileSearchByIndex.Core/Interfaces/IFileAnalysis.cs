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
        Task<IEnumerable<string>> CreateFileIndexListAsync(List<string> files, Action<string>? updateHandler, CancellationToken token = default);
    }
}
