﻿using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Infrastructure.TextAnalysis.Services
{
    public class CHTextAnalysisService : TxtAnalysisBase<CHTextAnalysisService>, IAnalysisService
    {
        public string FileExtension => ".txt.cn";
        protected override Regex WordSearchingRegex => CreateRegex(@"(?<word>\b[\u4e00-\u9fff]{2,})(.*?)(\k<word>)");
        public CHTextAnalysisService(IOptions<TaskThreadSettings> TaskSettings
            , IOptions<List<InboundFileConfig>> configs, IOptions<AppSettings> AppSettings)
            :base(AppSettings)
        {
            _taskSettings = TaskSettings.Value;
            Config = configs?.Value.FirstOrDefault(x => x?.FileExtension?.Equals(FileExtension, StringComparison.OrdinalIgnoreCase) ?? false);
            _repeatKeywordsConfig = Config?.GetRepeatKeywordsConfig() ?? new Dictionary<int, int>();
            _minWordLength = _repeatKeywordsConfig.Count > 0 ? _repeatKeywordsConfig.Min(x => x.Key) : 0;
            InitCharEncoding(Config?.EncodingName);
        }

        public async Task<KeywordRefSampleTxtModel> AnalysisFileKeyWorks(string file, Action<string>? updateHandler, CancellationToken token = default)
        {
            KeywordRefSampleTxtModel keyWords = new();
            try
            {
                var txt = await ReadFileAsync(file);
            }
            catch (Exception)
            {
                throw;
            }
            return keyWords;
        }
    }
}
