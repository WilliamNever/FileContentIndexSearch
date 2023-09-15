using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Settings
{
    public class AppSettings
    {
        public bool RemainTmpWorkingFIles { get; set; }
        public bool IsAppendFullWidthCharacters { get; set; }
        public string SuffixForFullWidthChrFile { get; set; } = null!;
    }
}
