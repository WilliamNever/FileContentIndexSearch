using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Interfaces
{
    public interface IAutoResetService : IDisposable
    {
        Task<T> RunAutoResetMethodAsync<T>(Func<CancellationToken, Task<T>> func, CancellationToken token = default) where T : class;
        void Set();
        void WaitOne();
    }
}
