namespace FileSearchByIndex.Core.Models
{
    public class IndexFilesModel
    {
        public List<SingleFileIndexModel> IndexFiles { get; set; } = new List<SingleFileIndexModel>();
        public string? Description { get; set; }
        public string IndexOfFolder { get; set; } = null!;
        public string? Filters { get; set; }
    }
}
