using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;

namespace FileSearchByIndex.Infrastructure.Services
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
            return await Task.Run(async () => { return await func.Invoke(source.Token); }, source.Token);
        }
    }
}