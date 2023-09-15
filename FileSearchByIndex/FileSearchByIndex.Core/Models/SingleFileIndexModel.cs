using FileSearchByIndex.Core.Enums;

namespace FileSearchByIndex.Core.Models
{
    public class SingleFileIndexModel
    {
        public string FileFullName { get; set; } = null!;

        [System.Text.Json.Serialization.JsonIgnore]
        public string FileShortName { get { return Path.GetFileName(FileFullName); } }
        [System.Text.Json.Serialization.JsonIgnore]
        public string FileExtension { get { return Path.GetExtension(FileFullName); } }
        public string FileVersion { get; set; } = null!;

        public List<KeyWordsModel> KeyWords { get; set; } = new List<KeyWordsModel>();
        public List<SampleTxtModel> SampleTxts { get; set; } = new List<SampleTxtModel>();

        public List<KeyWordsModel> GetKeyWordsByType(EnKeyWordsType enKeyWordsType) => KeyWords.Where(x => x.KeyWordsType == enKeyWordsType).ToList();
    }
}
