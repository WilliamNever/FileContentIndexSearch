using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Helper;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace FileSearchByIndex.Core.Services
{
    public class BaseService<TB> where TB : class
    {
        protected log4net.ILog _log = log4net.LogManager.GetLogger(typeof(TB));

        public async Task<List<RT?>> RunParallelForEach<RT, S>(IEnumerable<S> sources, Func<S, CancellationToken, Task<RT?>> func, int MaxDegreeOfParallelism, CancellationToken token = default)
        {
            return await RunParallelForEach(sources, func, new ParallelOptions { CancellationToken = token, MaxDegreeOfParallelism = MaxDegreeOfParallelism });
        }
        public async Task<List<RT?>> RunParallelForEach<RT, S>(IEnumerable<S> sources, Func<S, CancellationToken, Task<RT?>> func, ParallelOptions parallelOptions)
        {
            List<RT?> result = new();
            await Parallel.ForEachAsync(sources, parallelOptions,
                    async (item, token) =>
                        await Task.Run(async () =>
                        {
                            try
                            {
                                if (token.IsCancellationRequested) 
                                    throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                                RT? rsl = await func.Invoke(item, token);
                                lock (result) { result.Add(rsl); }
                            }
                            catch (TaskCanceledException cnclEx)
                            {
                                cnclEx.Data.Add("Source", item);
                                _log.Error($"Cancel Task ...", cnclEx);
                                throw;
                            }
                            catch (Exception ex)
                            {
                                ex.Data.Add("Source", item);
                                _log.Error($"Error in Parallel ...", ex);
                                throw;
                            }
                            finally
                            {
                            }
                        }
                    , parallelOptions.CancellationToken));

            return result;
        }

        protected async Task CreateJsonFileAsync<T>(string IndexFileFullName, string baseFolder, T indexForPath, string fileSuffix = "", JsonSerializerOptions? option = null)
        {
            if (!Directory.Exists(baseFolder)) Directory.CreateDirectory(baseFolder!);
            using (StreamWriter sw = new StreamWriter($"{IndexFileFullName}{fileSuffix}", false, System.Text.Encoding.UTF8))
            {
                sw.Write(
                    option == null ?
                    ConversionsHelper.SerializeToFormattedJson(indexForPath!)
                    : ConversionsHelper.SerializeToJson(indexForPath!, option)
                    );
                await sw.FlushAsync();
            }
        }
    }
}
