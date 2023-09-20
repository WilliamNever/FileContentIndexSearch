using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Services
{
    public class TaskHealthService : BaseService<TaskHealthService>, ITaskHealthService
    {
        protected AppSettings _appSettings;
        public TaskHealthService(IOptions<AppSettings> AppSettings)
        {
            _appSettings = AppSettings.Value;
        }

        public async Task<TResult> RunHealthTaskAysnc<TResult>(Func<CancellationToken, Task<TResult>> func, CancellationToken token = default)
        {
            CancellationTokenSource source;
            if (_appSettings.AnalysisOneFileTimeoutInMinutes > 0)
                source = new CancellationTokenSource(TimeSpan.FromMinutes(_appSettings.AnalysisOneFileTimeoutInMinutes));
            else
                source = new CancellationTokenSource();
            token.Register(() => source?.Cancel());
            try
            {
                return await Task.Run(async () => { return await func.Invoke(source.Token); }, source.Token);
            }
            catch (OperationCanceledException)
            {
                //todo: to do something when task was cancelled.
                throw;
            }
        }
    }
}
