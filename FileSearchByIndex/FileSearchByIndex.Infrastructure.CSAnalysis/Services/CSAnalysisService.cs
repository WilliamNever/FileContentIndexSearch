using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Services;

namespace FileSearchByIndex.Infrastructure.CSAnalysis.Services
{
    public class CSAnalysisService: BaseService<CSAnalysisService>, IAnalysisService
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileExtension { get => ".cs"; }

        public async Task<IEnumerable<KeyWordsModel>> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            List<KeyWordsModel> keyWords = new List<KeyWordsModel>();
            try
            {
                var txt = await ReadFileAsync(file);
                var rsl = await Task.WhenAll(
                    GetCommentKeyWordsAsync(txt, updateHandler, token),
                    GetNameKeyWordsAsync(txt, updateHandler, token),
                    GetCommandKeyWordsAsync(txt, updateHandler, token));

                foreach (var keyWord in rsl)
                    if (keyWord != null) keyWords.AddRange(keyWord);
                
            }
            catch (Exception)
            {
                throw;
            }
            return keyWords;
        }

        private async Task<IEnumerable<KeyWordsModel>> GetCommandKeyWordsAsync(string txt, Action<string>? updateHandler, CancellationToken token)
        {
            /*
             * pick up the commands in use
             */
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<KeyWordsModel>> GetNameKeyWordsAsync(string txt, Action<string>? updateHandler, CancellationToken token)
        {
            /*
             * Pick up the class, method names
             */
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="updateHandler"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<IEnumerable<KeyWordsModel>> GetCommentKeyWordsAsync(string txt, Action<string>? updateHandler, CancellationToken token)
        {
            /*
             * 1- 
                    /// <summary>
                    /// 
                    /// </summary>
                2-
                    /*  *_/
                3-
                    ///
             */
            throw new NotImplementedException();
        }

        private async Task<string> ReadFileAsync(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path, true))
                {
                    return sr.ReadToEnd();
                }
            }catch (Exception ex)
            {
                _log.Error($"Error(s) in Reading file {path}", ex);
                throw;
            }
        }
    }
}
