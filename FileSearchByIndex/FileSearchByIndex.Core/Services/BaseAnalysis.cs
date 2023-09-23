using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Core.Services
{
    public class BaseAnalysis<T> : BaseService<T> where T : class
    {
        protected object LockObj = new();
        protected AppSettings _appSettings;
        private double RegexTimeout;
        protected Regex EmptyChars { get => CreateRegex(@"[\s]+"); }
        protected Regex LineWrap { get => CreateRegex($"((\r)?{EnviConst.SpecNewLine1})"); }
        protected Regex Paragraph { get => CreateRegex($"((\r)?{EnviConst.SpecNewLine1}){{2,}}"); }
        protected Encoding FileEncoding { get; private set; } = Encoding.UTF8;

        public BaseAnalysis(IOptions<AppSettings> AppSettings)
        {
            _appSettings = AppSettings.Value;
            RegexTimeout = CalculateRegexMatchTimeout(_appSettings.AnalysisOneFileTimeoutInMinutes);
        }

        private double CalculateRegexMatchTimeout(int analysisOneFileTimeoutInMinutes)
        {
            double rto = analysisOneFileTimeoutInMinutes / 4d;
            if (analysisOneFileTimeoutInMinutes < 1) rto = -1;
            else rto = rto < 1 ? 1 : rto;
            return rto;
        }

        protected void InitCharEncoding(string? charCodeing)
        {
            try
            {
                if (string.IsNullOrEmpty(charCodeing) || charCodeing.Equals("default", StringComparison.OrdinalIgnoreCase))
                {
                    FileEncoding = Encoding.Default;
                }
                else
                {
                    var enc = Encoding.GetEncoding(charCodeing);
                    FileEncoding = enc;
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Failed to create Encoding with the name '{charCodeing}'", ex);
            }
        }

        protected async Task<string> ReadFileAsync(string path, int? LineNum = null)
        {
            try
            {
                using (StreamReader sr = new(path, FileEncoding, true
                    , new FileStreamOptions() { Mode = FileMode.Open, Access = FileAccess.Read, Share = FileShare.ReadWrite }
                    ))
                {
                    if (!LineNum.HasValue)
                        return await sr.ReadToEndAsync();
                    else
                    {
                        string lines = "";
                        for (int i = 0; i < LineNum; i++)
                        {
                            if (sr.EndOfStream)
                                break;
                            else
                                lines += await sr.ReadLineAsync();
                        }
                        return lines;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Error(s) in Reading file {path}", ex);
                throw;
            }
        }
        protected string ClearString(string txt, string regExpString, bool ignoreCase = true, string? appendString = null)
        {
            var regDCom = ignoreCase ? CreateRegex($"{regExpString}", RegexOptions.IgnoreCase)
                : CreateRegex($"{regExpString}");
            var regString = ignoreCase ? CreateRegex($"{regExpString}[\\w\\W]*?{regExpString}", RegexOptions.IgnoreCase)
                : CreateRegex($"{regExpString}[\\w\\W]*?{regExpString}");
            if ((regDCom.Matches(txt).Count & 1) > 0)
            {
                txt += (appendString ?? regExpString);
            }
            var str = regString.Replace(txt, "");
            return str;
        }

        protected KeyWordsModel CreateKeyword(string ori_txt, Match m, Core.Enums.EnKeyWordsType kvType, params char[]? trimEndChars)
        {
            var kwd = EmptyChars.Replace(LineWrap.Replace(m.Value, ""), " ").Trim();
            if (trimEndChars != null && trimEndChars.Any()) kwd = kwd.TrimEnd(trimEndChars);

            var kv = new KeyWordsModel
            {
                KeyWord = kwd,
                KeyWordsType = kvType,
            };
            kv.SampleTxts.Add(new SampleTxtModel { LineNumber = GetCurrentLineNumber(ori_txt, m.Value.Trim(), m) + 1, Text = m.Value.Trim().TrimEnd('{') });
            return kv;
        }
        protected int GetCurrentLineNumber(string ori_txt, string partTxt, Match match)
        {
            return LineWrap.Matches(ori_txt[0..ori_txt.IndexOf(partTxt?.Trim() ?? "", match.Index)]).Count;
        }
        private TimeSpan? GetRegexTimeout(double dv)
        {
            return dv < 0 ? null : TimeSpan.FromMinutes(dv);
        }
        protected Regex CreateRegex(string pattern, RegexOptions options = RegexOptions.None)
        {
            return CreateRegex(pattern, options, GetRegexTimeout(RegexTimeout));
        }
        private Regex CreateRegex(string pattern, RegexOptions options, TimeSpan? timeout)
        {
            return new Regex(pattern, options, timeout ?? Regex.InfiniteMatchTimeout);
        }
        protected void AddKeywordToKeyRef(KeywordRefSampleTxtModel keywordRef, KeyWordsModel kwordModel)
        {
            var existSpTxt = kwordModel.SampleTxts.Where(k =>
                keywordRef.SampleTxts.Any(kr => kr.LineNumber == k.LineNumber && k.Length > kr.Length));
            var notExistSpTxt = kwordModel.SampleTxts.Where(k =>
                !keywordRef.SampleTxts.Any(kr => kr.LineNumber == k.LineNumber));
            if (existSpTxt.Any())
            {
                keywordRef.SampleTxts.RemoveAll(kr => existSpTxt.Any(k => k.LineNumber == kr.LineNumber));
                keywordRef.SampleTxts.AddRange(existSpTxt);
            }
            keywordRef.SampleTxts.AddRange(notExistSpTxt);

            kwordModel.SampleTxts.Clear();
            keywordRef.KeyWords.Add(kwordModel);
        }

    }
}
