using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Models
{
    public class KeywordRefSampleTxtModel
    {
        public List<SampleTxtModel> SampleTxts { get; set; } = new List<SampleTxtModel>();
        public List<KeyWordsModel> KeyWords { get; set; } = new List<KeyWordsModel>();
    }
}
