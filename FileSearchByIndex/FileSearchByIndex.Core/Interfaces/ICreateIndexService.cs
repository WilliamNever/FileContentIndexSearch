using FileSearchByIndex.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Interfaces
{
    public interface ICreateIndexService
    {
        Task<string> CreateIndexFileAsync(SearchModel search, Action<string>? updateHandler = null, CancellationToken token = default);
    }
}
