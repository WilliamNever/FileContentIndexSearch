using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Helper;
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
        public string BatchID { get; protected set; }
        public FileAnalysis(IOptions<TaskThreadSettings> TaskSettings, Func<string, IAnalysisService?> getAnalyses)
        {
            _getAnalyses = getAnalyses;
            _taskSettings = TaskSettings.Value;
        }

        private async Task<string> CreateSingleFileIndexAsync(string batchid, string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            SingleFileIndexModel sfi = new SingleFileIndexModel { FileFullName = file };
            string tmpFileName = $"{batchid}_{file.ToMD5()}.json";

            var analysis = _getAnalyses(Path.GetExtension(file));
            if (analysis != null)
            {
                IEnumerable<KeyWordsModel> keyWords = await analysis.AnalysisFileKeyWorks(file, updateHandler, token);
                if (keyWords != null)
                {
                    sfi.KeyWords.AddRange(keyWords);
                }
                updateHandler?.Invoke($"The file {file} was analysised - .");
            }
            else
            {
                updateHandler?.Invoke($"The analysis Service does not exist for the file {file}.");
            }
            
            return WriteSingleAnalysisFile(EnviConst.TmpWorkingFolderPath, tmpFileName, sfi);
        }
        private string WriteSingleAnalysisFile(string BaseFolder, string fileName, object sfi)
        {
            if (!Directory.Exists(BaseFolder)) Directory.CreateDirectory(BaseFolder);
            string fullFileName = Path.Combine(BaseFolder, fileName);
            using (StreamWriter sw = new StreamWriter(fullFileName, false, System.Text.Encoding.UTF8))
            {
                sw.Write(ConversionsHelper.SerializeToFormattedJson(sfi));
                sw.Flush();
            }
            return fullFileName;
        }

        public async Task<IEnumerable<string>> CreateFileIndexListAsync(List<string> files, Action<string>? updateHandler, CancellationToken token = default)
        {
            BatchID = Guid.NewGuid().ToString().Replace("-", "");
            List<string> list = new List<string>();
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
                                var singleFileIndex = await CreateSingleFileIndexAsync(BatchID, item, updateHandler, token);
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
