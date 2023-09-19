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
    public class FileAnalysis : BaseService<FileAnalysis>, IFileAnalysis
    {
        protected Func<string, IAnalysisService?> _getAnalyses = null!;
        protected TaskThreadSettings _taskSettings = null!;
        public string BatchID { get; protected set; } = null!;
        protected readonly AppSettings _appSettings;
        public FileAnalysis(IOptions<TaskThreadSettings> TaskSettings, IOptions<AppSettings> AppSettings, Func<string, IAnalysisService?> getAnalyses)
        {
            _getAnalyses = getAnalyses;
            _taskSettings = TaskSettings.Value;
            _appSettings = AppSettings.Value;
        }

        private async Task<string[]> CreateSingleFileIndexAsync(string batchid, string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            //var dtN = DateTime.Now;
            string fileVersion = $"{batchid}_{file.ToMD5()}";
            SingleFileIndexModel sfi = new SingleFileIndexModel { FileFullName = file, FileVersion = fileVersion };
            string tmpFileName = $"{fileVersion}.json";

            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException($"Task {Thread.CurrentThread.ManagedThreadId} is Canceled at {DateTime.Now}");
            }

            var analysis = _getAnalyses(Path.GetExtension(file));
            if (analysis != null)
            {
                try
                {
                    var keyWords = (await analysis.AnalysisFileKeyWorks(file, updateHandler, token)).ToList();
                    if (keyWords != null && keyWords.Any())
                    {
                        sfi.KeyWords.AddRange(keyWords);
                        var smpls = keyWords.SelectMany(x => x.SampleTxts);
                        var grps = smpls.GroupBy(x => x.LineNumber).OrderBy(x => x.Key);
                        foreach (var grp in grps)
                        {
                            sfi.SampleTxts.Add(grp.OrderByDescending(x => x.Length).First());
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {
                    sfi.LoadingMessage = $"Failed to loading Messages for time out - {fileVersion} - {ex.Message}";
                    updateHandler?.Invoke($"File Operation was Canceled for time out. - {fileVersion} - File {file} - {ex.Message} - {EnviConst.EnvironmentNewLine}");
                    ex.Data.Add("FileVersion", $"{fileVersion}");
                    _log.Error($"Failed to Analysis for time out!", ex);
                }
                catch (Exception ex)
                {
                    sfi.LoadingMessage = $"Failed to loading Messages - {fileVersion} - {ex.Message}";
                    updateHandler?.Invoke($"{file} broke - {fileVersion} - {ex.Message} - {EnviConst.EnvironmentNewLine}");
                    ex.Data.Add("FileVersion", $"{fileVersion}");
                    _log.Error($"Failed to pick up command keyword from {file}", ex);
                }
            }
            else
            {
                updateHandler?.Invoke($"The analysis Service does not exist for the file {file}.");
            }
            //var dtFN = DateTime.Now;
            //_log.Info($"Batch id - {batchid}, File - {file}, Begin - {dtN}, Finished - {dtFN}, Cost {(dtFN-dtN).TotalMinutes} Minutes.");
            var fp = Path.Combine(EnviConst.TmpWorkingFolderPath, tmpFileName);
            List<string> filenames = new List<string> { fp };
            await CreateJsonFileAsync(fp, EnviConst.TmpWorkingFolderPath, sfi);
            if (_appSettings.IsAppendFullWidthCharacters)
            {
                await CreateJsonFileAsync(fp, EnviConst.TmpWorkingFolderPath, sfi, _appSettings.SuffixForFullWidthChrFile, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                filenames.Add($"{fp}{_appSettings.SuffixForFullWidthChrFile}");
            }
            return filenames.ToArray();
        }

        public async Task<IEnumerable<string>> CreateFileIndexListAsync(List<string> files, Action<string>? updateHandler, CancellationToken token = default)
        {
            BatchID = Guid.NewGuid().ToString().ToMD5();
            List<string> list = new List<string>();
            try
            {
                if (files != null && files.Any())
                    await Parallel.ForEachAsync(files, new ParallelOptions { MaxDegreeOfParallelism = _taskSettings.TaskInitCount, CancellationToken = token },
                        async (item, token) =>
                            await Task.Run(async () =>
                            {
                                if (token.IsCancellationRequested)
                                {
                                    return;
                                }
                                var singleFileIndex = await CreateSingleFileIndexAsync(BatchID, item, updateHandler, token);
                                if (singleFileIndex != null)
                                    lock (list)
                                    {
                                        list.AddRange(singleFileIndex);
                                    }
                            }
                        , token));
            }
            catch (OperationCanceledException ex)
            {
                _log.Error($"Batch Id - {BatchID} was Canceled - ", ex);
            }
            catch (Exception ex)
            {
                _log.Error($"Batch Id - {BatchID} was broken - ", ex);
            }
            return list;
        }
    }
}
