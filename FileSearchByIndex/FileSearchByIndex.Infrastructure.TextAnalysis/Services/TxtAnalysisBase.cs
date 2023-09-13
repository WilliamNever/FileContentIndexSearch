using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
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
        protected InboundFileConfig? Config;
        protected TaskThreadSettings _taskSettings = null!;

        private Regex CheckChineseRegex { get => new(@"[\u4e00-\u9fa5]"); }
        protected virtual Regex WordSearchingRegex { get; } = null!;
        /// <summary>
        /// how many times each keyword in different length should appear in the text.
        /// </summary>
        protected Dictionary<int, int> _repeatKeywordsConfig = null!;
        /// <summary>
        /// the valid min length of the keyword
        /// </summary>
        protected int _minWordLength;

        public bool HasChineseInFileName(string fileName)
        {
            return CheckChineseRegex.IsMatch(fileName);
        }
    }
}
