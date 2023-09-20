using FileSearchByIndex.Core.Interfaces;

namespace FileSearchByIndex.Core.Services
{
    public class AutoResetService<T> : BaseService<AutoResetService<T>>, IAutoResetService<T> where T : class
    {
        protected AutoResetEvent autoReset = new AutoResetEvent(false);
        public async Task<T> RunAutoResetMethodAsync(Func<CancellationToken, Task<T>> func, CancellationToken token = default)
        {
            token.Register(() => Set());
            var rsl = await func.Invoke(token);
            Set();
            return rsl;
        }
        public void WaitOne()
        {
            autoReset.Reset();
            autoReset.WaitOne();
        }
        public void Set() => autoReset.Set();

        void IDisposable.Dispose()
        {
            autoReset.Close();
        }
    }
}
