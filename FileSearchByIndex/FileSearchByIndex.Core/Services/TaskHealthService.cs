﻿using FileSearchByIndex.Core.Interfaces;
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
            var source = CreateRelatedTokenSource(token);

            TResult rsl;
            try
            {
                var task = _autoResetService.RunAutoResetMethodAsync(
                          async xtk => await func.Invoke(xtk), source.Token);
                _autoResetService.WaitOne();
                if (source.Token.IsCancellationRequested)
                    throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                rsl = await task;

            }
            catch (OperationCanceledException)
            {
                //todo: to do something when task was cancelled.
                source.Cancel();
                throw;
            }
            catch (Exception)
            {
                //todo: to do something when task was cancelled.
                source.Cancel();
                throw;
            }
            return rsl;
        }
        public async Task RunHealthTaskWithAutoRestWaitAysnc(Func<CancellationToken, Task> func, CancellationToken token = default)
        {
            var source = CreateRelatedTokenSource(token);

            try
            {
                var task = _autoResetService.RunAutoResetMethodAsync(
                          async xtk => await func.Invoke(xtk), source.Token);
                _autoResetService.WaitOne();
                if (source.Token.IsCancellationRequested)
                    throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                await task;

            }
            catch (OperationCanceledException)
            {
                //todo: to do something when task was cancelled.
                source.Cancel();
                throw;
            }
            catch (Exception)
            {
                //todo: to do something when task was cancelled.
                source.Cancel();
                throw;
            }
        }
        public async Task<TResult> RunHealthTaskAysnc<TResult>(Func<CancellationToken, Task<TResult>> func, CancellationToken token = default) where TResult : class
        {
            var source = CreateRelatedTokenSource(token);
            try
            {
                return await Task.Run(async () => { return await func.Invoke(source.Token); }, source.Token);
            }
            catch (OperationCanceledException)
            {
                //todo: to do something when task was cancelled.
                source.Cancel();
                throw;
            }
            catch (Exception)
            {
                //todo: to do something when task was cancelled.
                source.Cancel();
                throw;
            }
        }

        private CancellationTokenSource CreateRelatedTokenSource(CancellationToken token)
        {
            CancellationTokenSource source;
            if (_appSettings.AnalysisOneFileTimeoutInMinutes > 0)
                source = new CancellationTokenSource(TimeSpan.FromMinutes(_appSettings.AnalysisOneFileTimeoutInMinutes));
            else
                source = new CancellationTokenSource();
            token.Register(() => source?.Cancel());
            return source;
        }

        void IDisposable.Dispose()
        {
            _autoResetService.Dispose();
        }
    }
}
