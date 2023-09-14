using FileSearchByIndex.Core.Enums;

namespace FileSearchByIndex.Core.Models
{
    public class KeyWordsModel
    {
        public EnKeyWordsType KeyWordsType { get; set; } = EnKeyWordsType.None;
        public string KeyWord { get; set; } = null!;
        [System.Text.Json.Serialization.JsonIgnore]
        public int Frequency { get => SampleTxts.Count; }
        public List<SampleTxtModel> SampleTxts { get; set; } = new List<SampleTxtModel>();
    }
}
