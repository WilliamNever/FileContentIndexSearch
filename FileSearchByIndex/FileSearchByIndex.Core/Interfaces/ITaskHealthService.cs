using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Interfaces
{
    public interface ITaskHealthService : IDisposable
    {
        Task<TResult> RunHealthTaskAysnc<TResult>(Func<CancellationToken, Task<TResult>> func, CancellationToken token = default) where TResult : class;
        Task<TResult> RunHealthTaskWithAutoRestWaitAysnc<TResult>(Func<CancellationToken, Task<TResult>> func, CancellationToken token = default) where TResult : class;
    }
}
