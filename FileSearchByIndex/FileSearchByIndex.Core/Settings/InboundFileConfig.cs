namespace FileSearchByIndex.Core.Settings
{
    public class InboundFileConfig
    {
        public string FileExtension { get; set; } = null!;
        public string? EncodingName { get; set; }
        /// <summary>
        /// if CanAutoSelectAnalysisService == true, one of analysises in the ServiceExtensions will be selected to analysis the inbound file.
        /// if CanAutoSelectAnalysisService == true, all of analysises in the ServiceExtensions will be used on the inbound file.
        /// default is false
        /// </summary>
        public bool CanAutoSelectAnalysisService { get; set; } = false;
        /// <summary>
        /// the analysises list can be used on the inbound file.
        /// </summary>
        public string[]? ServiceExtensions { get; set; }
    }
}
