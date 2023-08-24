using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using System.Net.NetworkInformation;

namespace FileSearchByIndex.Infrastructure.Services
{
    public class CreateIndexService : BaseService<CreateIndexService>, ICreateIndexService
    {
        protected IFileAnalysis _fileAnaly;
        public CreateIndexService(IFileAnalysis fileAnalysis)
        {
            _fileAnaly = fileAnalysis;
        }
        public async Task<string> CreateIndexFileAsync(SearchModel search, Action<string>? updateHandler = null, CancellationToken token = default)
        {
            for (var i = 0; i < 3; i++)
            {
                if (token.IsCancellationRequested) throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                Thread.Sleep(1000);
            }
            var sf = string.IsNullOrEmpty(search.Filter) ? throw new NotImplementedException() : search.Filter;
            updateHandler?.Invoke(sf);
            return "path string" + sf;
        }

        private void SearchDirectories(string[] SearchDirectories, SearchModel searchInforModel, Action<string>? updateHandler = null, CancellationToken token = default)
        {
            //string[] SearchDirectories = new string[] { searchInforModel.SearchPath };
            List<string> files = new List<string>();
            List<string> directories = new List<string>();
            string searchFolder;

            foreach (var path in SearchDirectories)
            {
                searchFolder = path;
                
                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                }

                try
                {
                    files.AddRange(Directory.GetFiles(searchFolder, searchInforModel.Filter));
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
                try
                {
                    directories.AddRange(Directory.GetDirectories(searchFolder));
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
            if (searchInforModel.IsIncludeSub && directories.Any())
            {
                this.SearchDirectories(directories.ToArray(), searchInforModel, updateHandler, token);
            }
        }
    }
}
