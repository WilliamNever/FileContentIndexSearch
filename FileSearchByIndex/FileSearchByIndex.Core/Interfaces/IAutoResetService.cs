using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Interfaces
{
    public interface IAutoResetService<T> : IDisposable where T : class
    {
        Task<T> RunAutoResetMethodAsync(Func<CancellationToken, Task<T>> func, CancellationToken token = default);
        void Set();
        void WaitOne();
    }
}
