using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;

namespace FileSearchByIndex.Infrastructure.Services
{
    public class CreateIndexService : BaseService<CreateIndexService>, ICreateIndexService
    {
        public async Task<string> CreateIndexFileAsync(SearchModel search, Action<string>? updateHandler = null)
        {
            //_log.Error("mess", new Exception("asshh"));
            for (var i = 0; i < 3; i++)
                Thread.Sleep(1000);
            var sf = string.IsNullOrEmpty(search.Filter) ? throw new NotImplementedException() : search.Filter;
            updateHandler?.Invoke(sf);
            return "path string" + sf;
        }
    }
}
