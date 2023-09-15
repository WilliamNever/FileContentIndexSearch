using System.Diagnostics.CodeAnalysis;

namespace FileSearchByIndex.Core.Models
{
    public class SampleTxtModel : IEqualityComparer<SampleTxtModel>
    {
        public int LineNumber { get; set; }
        public string? Text { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public int Length { get => (Text ?? "").Length; }
        public bool Equals(SampleTxtModel? x, SampleTxtModel? y)
        {
            return x?.LineNumber == y?.LineNumber;
        }

        public int GetHashCode([DisallowNull] SampleTxtModel obj)
        {
            return GetHashCode();
        }
        public static IEqualityComparer<SampleTxtModel> GetComparer()
        {
            return new SampleTxtModel();
        }
    }
}
