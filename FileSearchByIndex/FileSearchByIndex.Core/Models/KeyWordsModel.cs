using FileSearchByIndex.Core.Enums;

namespace FileSearchByIndex.Core.Models
{
    public class KeyWordsModel
    {
        public EnKeyWordsType KeyWordsType { get; set; } = EnKeyWordsType.None;
        public string KeyWord { get; set; } = null!;
        [System.Text.Json.Serialization.JsonIgnore]
        public int Frequency { get => LineNumbers.Count; }
        public List<int> LineNumbers { get; set; } = new List<int>();

        [System.Text.Json.Serialization.JsonIgnore]
        public List<SampleTxtModel> SampleTxts { get; set; } = new List<SampleTxtModel>();
    }
}
