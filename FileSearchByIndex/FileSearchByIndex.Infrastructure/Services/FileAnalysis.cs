using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;

namespace FileSearchByIndex.Infrastructure.Services
{
    public class FileAnalysis : BaseService<FileAnalysis>, IFileAnalysis
    {
        protected Func<string, IAnalysisService?> _getAnalyses;
        protected TaskThreadSettings _taskSettings;
        public FileAnalysis(IOptions<TaskThreadSettings> TaskSettings, Func<string, IAnalysisService?> getAnalyses)
        {
            _getAnalyses = getAnalyses;
            _taskSettings = TaskSettings.Value;
        }

        public async Task<SingleFileIndexModel?> CreateSingleFileIndexAsync(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            SingleFileIndexModel sfi = new SingleFileIndexModel { FileFullName = file };
            var analysis = _getAnalyses(Path.GetExtension(file));
            if (analysis != null)
            {
                IEnumerable<KeyWordsModel> keyWords = await analysis.AnalysisFileKeyWorks(file, updateHandler, token);
                if (keyWords != null)
                {
                    sfi.KeyWords.AddRange(keyWords);
                }
            }
            else
            {
                updateHandler?.Invoke($"The analysis Service does not exist for the file {file}.");
            }
            return sfi;
        }
        public async Task<IEnumerable<SingleFileIndexModel>> CreateFileIndexListAsync(List<string> files, Action<string>? updateHandler, CancellationToken token = default)
        {
            List<SingleFileIndexModel> list = new List<SingleFileIndexModel>();
            if (files != null && files.Any())
                await Parallel.ForEachAsync(files, new ParallelOptions { MaxDegreeOfParallelism = _taskSettings.TaskInitCount },
                    async (item, cancellationToken) =>
                        await Task.Run(async () =>
                        {
                            try
                            {
                                if (token.IsCancellationRequested)
                                {
                                    throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
                                }
                                var singleFileIndex = await CreateSingleFileIndexAsync(item, updateHandler, token);
                                if (singleFileIndex != null)
                                    lock (list)
                                    {
                                        list.Add(singleFileIndex);
                                    }
                            }
                            catch (Exception ex)
                            {
                                _log.Error($"{item} broke - {EnviConst.EnvironmentNewLine}", ex);
                                updateHandler?.Invoke($"{item} broke - {ex.Message} - {EnviConst.EnvironmentNewLine}");
                            }
                            finally
                            {
                            }
                        }
                    , token));
            return list;
        }
    }
}
