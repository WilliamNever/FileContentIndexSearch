using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;

namespace FileSearchByIndex.Infrastructure.CSAnalysis.Services
{
    public class CSAnalysisService: IAnalysisService
    {
        public string FileType { get => ".cs"; }
        public async Task<SingleFileIndexModel> CreateFileIndexes()
        {
            return null;
        }
    }
}
