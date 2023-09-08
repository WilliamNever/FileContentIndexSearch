using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Interfaces
{
    public interface ITaskWorking<in T> where T : class
    {
        Task StartWorkingAsync(T Param, CancellationTokenSource? cts = null);
        Task CancelWorkingAsync(string workName = "NoName");
    }
}
