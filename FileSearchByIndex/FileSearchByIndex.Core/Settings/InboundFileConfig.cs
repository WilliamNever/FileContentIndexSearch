namespace FileSearchByIndex.Core.Settings
{
    public class InboundFileConfig: IEqualityComparer<AppValuesSetting>
    {
        public string FileExtension { get; set; } = null!;
        public string? EncodingName { get; set; }
        /// <summary>
        /// if CanAutoSelectAnalysisService == true, one of analysises in the ServiceExtensions will be selected to analysis the inbound file.
        /// if CanAutoSelectAnalysisService == false, just the common analysis will be used on the inbound file.
        /// default is false
        /// </summary>
        public bool CanAutoSelectAnalysisService { get; set; } = false;
        /// <summary>
        /// the analysises list can be used on the inbound file.
        /// </summary>
        public string[]? ServiceExtensions { get; set; }
        public List<AppValuesSetting> RepeatTimesAsKeywords { get; set; } = null!;

        public Dictionary<int, int>? GetRepeatKeywordsConfig()
        {
            return RepeatTimesAsKeywords?.Distinct(this).ToDictionary(x => x.Key, x => x.Value);
        }

        bool IEqualityComparer<AppValuesSetting>.Equals(AppValuesSetting? x, AppValuesSetting? y)
        {
            return x?.Key == y?.Key;
        }

        int IEqualityComparer<AppValuesSetting>.GetHashCode(AppValuesSetting obj)
        {
            return GetHashCode();
        }
    }
}
