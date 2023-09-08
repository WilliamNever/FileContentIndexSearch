﻿using FileSearchByIndex.Core.Consts;
using System.Text;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Core.Services
{
    public class BaseAnalysis<T> : BaseService<T> where T : class
    {
        protected Regex EmptyChars { get => new(@"[\s]+"); }
        protected Regex LineWrap { get => new($"({EnviConst.EnvironmentNewLine}|{EnviConst.SpecNewLine1})"); }
        protected Encoding FileEncoding { get; private set; } = Encoding.UTF8;
        public void InitCharEncoding(string charCodeing)
        {
            try
            {
                var enc = Encoding.GetEncoding(charCodeing);
                FileEncoding = enc;
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
    }
}
