using FileSearchByIndex.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileSearchByIndex.Infrastructure.Services
{
    public class AutoResetService : IAutoResetService
    {
        public async Task RunAutoResetMethod<T>(Func<CancellationToken, Task<T>> func, CancellationToken token = default) where T : class
        {
        }
    }
}
