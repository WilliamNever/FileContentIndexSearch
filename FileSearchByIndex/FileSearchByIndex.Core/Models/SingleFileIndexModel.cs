using FileSearchByIndex.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Models
{
    public class SingleFileIndexModel
    {
        public string FileFullName { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string FileShortName { get { return Path.GetFileName(FileFullName); } }
        [Newtonsoft.Json.JsonIgnore]
        public string FileExtension { get { return Path.GetExtension(FileFullName); } }

        public List<KeyWordsModel> KeyWords { get; set; } = new List<KeyWordsModel>();

        public List<KeyWordsModel> GetKeyWordsByType(EnKeyWordsType enKeyWordsType) => KeyWords.Where(x => x.KeyWordsType == enKeyWordsType).ToList();
    }
}
