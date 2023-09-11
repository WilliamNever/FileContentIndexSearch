using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Helper;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace FileSearchByIndex.Infrastructure.Services
{
    public class CreateIndexService : BaseService<CreateIndexService>, ICreateIndexService
    {
        protected IFileAnalysis _fileAnaly;
        protected TaskThreadSettings _taskSettings;
        protected readonly AppSettings _appSettings;
        public CreateIndexService(IOptions<TaskThreadSettings> TaskSettings, IOptions<AppSettings> AppSettings, IFileAnalysis fileAnalysis)
        {
            _fileAnaly = fileAnalysis;
            _taskSettings = TaskSettings.Value;
            _appSettings = AppSettings.Value;
        }
        public async Task<string> CreateIndexFileAsync(SearchModel search, Action<string>? updateHandler = null, CancellationToken token = default)
        {
            IndexFilesModel indexForPath = new IndexFilesModel()
            {
                Description = search.IndexDescription,
                IndexOfFolder = search.SearchPath
            };

            var files = SearchDirectories(new string[] { search.SearchPath }, search, updateHandler, token);
            List<string> PartailIndexFiles = new List<string>();
            if (files.Any())
            {
                try
                {
                    PartailIndexFiles.AddRange(await _fileAnaly.CreateFileIndexListAsync(files, updateHandler, token));

                    await Parallel.ForEachAsync(PartailIndexFiles, new ParallelOptions { MaxDegreeOfParallelism = _taskSettings.TaskInitCount },
                        async (item, cancellationToken) =>
                            await Task.Run( () =>
                            {
                                try
                                {
                                    if (token.IsCancellationRequested)
                                    {
                                        throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                                    }
                                    var singleFileIndex = ReadSingleIndexFromFile(item);
                                    if (singleFileIndex != null)
                                        lock (indexForPath.IndexFiles)
                                        {
                                            indexForPath.IndexFiles.Add(singleFileIndex);
                                        }
                                }
                                catch (Exception ex)
                                {
                                    _log.Error($"{item} broke - {EnviConst.EnvironmentNewLine}", ex);
                                    updateHandler?.Invoke($"{item} broke - {ex.Message} - {EnviConst.EnvironmentNewLine}");
                                }
                                finally
                                {
                                    if (!_appSettings.RemainTmpWorkingFIles && File.Exists(item)) File.Delete(item);
                                }
                            }
                        , token));
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (token.IsCancellationRequested)
                    {
                        foreach (var item in PartailIndexFiles) if (!_appSettings.RemainTmpWorkingFIles && File.Exists(item)) File.Delete(item);
                        throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                    }
                }
            }

            await CreateIndexJsonFileAsync(search, indexForPath);

            updateHandler?.Invoke($"{search.IndexFileFullName} is created - ");
            return search.IndexFileFullName;
        }

        private SingleFileIndexModel? ReadSingleIndexFromFile(string item)
        {
            if (File.Exists(item))
                using (StreamReader reader = new StreamReader(item))
                {
                    return ConversionsHelper.DeserializeJson<SingleFileIndexModel>(reader.ReadToEnd());
                }
            else
                return null;
        }

        private async Task CreateIndexJsonFileAsync(SearchModel search, IndexFilesModel indexForPath)
        {
            if (!Directory.Exists(EnviConst.IndexesFolderPath)) Directory.CreateDirectory(EnviConst.IndexesFolderPath);
            using (StreamWriter sw = new StreamWriter(search.IndexFileFullName, false, System.Text.Encoding.UTF8))
            {
                sw.Write(ConversionsHelper.SerializeToFormattedJson(indexForPath));
                await sw.FlushAsync();
            }
        }

        private List<string> SearchDirectories(string[] searchDir, SearchModel searcher, Action<string>? updateHandler = null, CancellationToken token = default)
        {
            List<string> files = new();
            List<string> directories = new();

            string[] filefilters;
            if (string.IsNullOrEmpty(searcher.Filter))
                filefilters = new string[] { "*.*" };
            else
                filefilters = searcher.Filter.Split('|', StringSplitOptions.RemoveEmptyEntries);


            foreach (var searchFolder in searchDir)
            {
                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                }

                try
                {
                    foreach (var f in filefilters) files.AddRange(Directory.GetFiles(searchFolder, f));
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message, ex);
                }
                try
                {
                    directories.AddRange(Directory.GetDirectories(searchFolder));
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message, ex);
                }
            }
            if (searcher.IsIncludeSub && directories.Any())
            {
                files.AddRange(SearchDirectories(directories.ToArray(), searcher, updateHandler, token));
            }
            return files;
        }
    }
}
