using FileSearchByIndex.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public abstract class TxtAnalysisBase<T> : BaseAnalysis<T> where T : class
    {
        private Regex CheckChineseRegex { get => new(@"[\u4e00-\u9fa5]"); }
        protected virtual Regex WordSearchingRegex { get; } = null!;
        public bool HasChineseInFileName(string fileName)
        {
            return CheckChineseRegex.IsMatch(fileName);
        }
    }
}
