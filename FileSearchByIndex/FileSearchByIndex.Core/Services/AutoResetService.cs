using FileSearchByIndex.Core.Interfaces;

namespace FileSearchByIndex.Core.Services
{
    public class AutoResetService : BaseService<AutoResetService>, IAutoResetService
    {
        protected AutoResetEvent? autoReset = new AutoResetEvent(false);

        public async Task<T> RunAutoResetMethodAsync<T>(Func<CancellationToken, Task<T>> func, CancellationToken token = default) where T : class
        {
            autoReset?.Reset();
            token.Register(() => 
            Set()
            );
            T rsl; try
            {
                rsl = await func.Invoke(token);
            }
            catch (Exception)
            { 
                throw; 
            }
            finally 
            { 
                Set(); 
            }
            
            return rsl;
        }
        public void WaitOne()
        {
            autoReset?.WaitOne();
        }
        public void Set() => autoReset?.Set();

        void IDisposable.Dispose()
        {
            autoReset?.Close();
            autoReset = null;
        }
    }
}
