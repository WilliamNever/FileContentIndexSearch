using FileSearchByIndex.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Models
{
    public class KeyWordsModel
    {
        public EnKeyWordsType KeyWordsType { get; set; } = EnKeyWordsType.None;
        public string KeyWord { get; set; }
        public int Frequency { get; set; }
        public List<SampleTxtModel> SampleTxts { get; set; } = new List<SampleTxtModel>();
    }
}
