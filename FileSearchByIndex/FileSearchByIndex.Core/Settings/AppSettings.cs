using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Settings
{
    public class AppSettings : IOptions<AppSettings>
    {
        public bool RemainTmpWorkingFIles { get; set; }
        public bool IsAppendFullWidthCharacters { get; set; }
        public string SuffixForFullWidthChrFile { get; set; } = null!;
        /// <summary>
        /// if AnalysisOneFileTimeoutInMinutes<=0, it means there is no limitation for time out.
        /// </summary>
        public int AnalysisOneFileTimeoutInMinutes { get; set; }

        public AppSettings Value => this;
    }
}
