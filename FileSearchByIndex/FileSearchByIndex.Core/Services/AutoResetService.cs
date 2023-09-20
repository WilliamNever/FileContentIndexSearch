using FileSearchByIndex.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Services
{
    public class AutoResetService<T> : BaseService<AutoResetService<T>>, IAutoResetService<T> where T : class
    {
        public async Task RunAutoResetMethod(Func<CancellationToken, Task<T>> func, CancellationToken token = default)
        {
        }
    }
}
