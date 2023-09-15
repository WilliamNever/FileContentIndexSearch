using FileSearchByIndex.Core.Consts;
using static System.Net.Mime.MediaTypeNames;

namespace FileSearchByIndex.Core.Services
{
    public class BaseService<TB> where TB : class
    {
        protected log4net.ILog _log = log4net.LogManager.GetLogger(typeof(TB));

        public async Task<List<RT?>> RunParallelForEach<RT, S>(IEnumerable<S> sources, Func<S, Task<RT?>> func, int MaxDegreeOfParallelism, CancellationToken token = default)
        {
            return await RunParallelForEach(sources, func, new ParallelOptions { CancellationToken = token, MaxDegreeOfParallelism = MaxDegreeOfParallelism });
        }
        public async Task<List<RT?>> RunParallelForEach<RT, S>(IEnumerable<S> sources, Func<S, Task<RT?>> func, ParallelOptions parallelOptions)
        {
            List<RT?> result = new();
            await Parallel.ForEachAsync(sources, parallelOptions,
                    async (item, token) =>
                        await Task.Run(async () =>
                        {
                            try
                            {
                                if (token != CancellationToken.None && token.IsCancellationRequested) return;
                                RT? rsl = await func.Invoke(item);
                                lock (result) { result.Add(rsl); }
                            }
                            catch (TaskCanceledException cnclEx)
                            {
                                cnclEx.Data.Add("Source", item);
                                _log.Error($"Cancel Task ...", cnclEx);
                            }
                            catch (Exception ex)
                            {
                                ex.Data.Add("Source", item);
                                _log.Error($"Error in Parallel ...", ex);
                            }
                            finally
                            {
                            }
                        }
                    , parallelOptions.CancellationToken));

            return result;
        }
    }
}
