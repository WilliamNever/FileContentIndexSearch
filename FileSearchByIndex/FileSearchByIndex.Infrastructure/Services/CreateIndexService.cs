using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Helper;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;

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
            /*
             * if search.Filter is empty regards as *.*
             */

            var extList = (search.Filter ?? "").Split('|').ToList();
            extList.ForEach(x => x.GetFileExtension());

            IndexFilesModel indexForPath = new IndexFilesModel()
            {
                Description = search.IndexDescription,
                IndexOfFolder = search.SearchPath,
                Filters = extList.Count < 1 ? "" : string.Join('|', extList)
            };

            var files = SearchDirectories(new string[] { search.SearchPath }, search, updateHandler, token);
            updateHandler?.Invoke($"There are {files.Count} files, going to be processed. {EnviConst.EnvironmentNewLine}");

            List<string> PartailIndexFiles = new List<string>();
            if (files.Any())
            {
                try
                {
                    PartailIndexFiles.AddRange(await _fileAnaly.CreateFileIndexListAsync(files, updateHandler, token));

                    await Parallel.ForEachAsync(PartailIndexFiles.Where(x => !x.EndsWith(_appSettings.SuffixForFullWidthChrFile))
                        , new ParallelOptions { MaxDegreeOfParallelism = _taskSettings.TaskInitCount, CancellationToken = token },
                        async (item, token) =>
                            await Task.Run(() =>
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
                            }
                        , token));
                }
                catch (Exception)
                {
                }
                finally
                {
                    foreach (var item in PartailIndexFiles)
                    {
                        if (!_appSettings.RemainTmpWorkingFIles && File.Exists(item)) File.Delete(item);
                    }
                    if (token.IsCancellationRequested)
                    {
                        throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                    }
                }
            }

            await CreateJsonFileAsync(search.IndexFileFullName, EnviConst.IndexesFolderPath, indexForPath);

            if (_appSettings.IsAppendFullWidthCharacters)
            {
                await CreateJsonFileAsync(search.IndexFileFullName, EnviConst.IndexesFolderPath, indexForPath, _appSettings.SuffixForFullWidthChrFile
                    , new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });
            }

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
