using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Core.Services
{
    public class BaseAnalysis<T> : BaseService<T> where T : class
    {
        protected Regex EmptyChars { get => new(@"[\s]+"); }
        protected Regex LineWrap { get => new($"((\\r)?{EnviConst.SpecNewLine1})"); }
        protected Regex Paragraph { get => new($"((\\r)?{EnviConst.SpecNewLine1}){{2,}}"); }
        protected Encoding FileEncoding { get; private set; } = Encoding.UTF8;
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
            var regDCom = ignoreCase ? new Regex($"{regExpString}", RegexOptions.IgnoreCase)
                : new Regex($"{regExpString}");
            var regString = ignoreCase ? new Regex($"{regExpString}[\\w\\W]*?{regExpString}", RegexOptions.IgnoreCase)
                : new Regex($"{regExpString}[\\w\\W]*?{regExpString}");
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
            kv.SampleTxts.Add(new SampleTxtModel { LineNumber = GetCurrentLineNumber(ori_txt, m.Value.Trim(), m), Text = m.Value.Trim().TrimEnd('{') });
            return kv;
        }
        protected int GetCurrentLineNumber(string ori_txt, string partTxt, Match match)
        {
            return LineWrap.Matches(ori_txt[0..ori_txt.IndexOf(partTxt?.Trim() ?? "", match.Index)]).Count + 1;
        }

    }
}
