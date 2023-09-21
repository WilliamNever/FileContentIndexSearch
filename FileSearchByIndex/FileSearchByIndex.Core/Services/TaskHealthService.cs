using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;

namespace FileSearchByIndex.Core.Services
{
    public class TaskHealthService : BaseService<TaskHealthService>, ITaskHealthService
    {
        protected AppSettings _appSettings;
        protected IAutoResetService _autoResetService;
        public TaskHealthService(IOptions<AppSettings> AppSettings, IAutoResetService autoResetService)
        {
            _appSettings = AppSettings.Value;
            _autoResetService = autoResetService;
        }

        public async Task<TResult> RunHealthTaskWithAutoRestWaitAysnc<TResult>(Func<CancellationToken, Task<TResult>> func, CancellationToken token = default) where TResult : class
        {
            return default(TResult);
        }
        public async Task<TResult> RunHealthTaskAysnc<TResult>(Func<CancellationToken, Task<TResult>> func, CancellationToken token = default) where TResult : class
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

        void IDisposable.Dispose()
        {
            _autoResetService.Dispose();
        }
    }
}
