using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Helper;
using FileSearchByIndex.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleTest.Tests
{
    public class MainTestThird
    {
        public async static Task Main3Async()
        {
            int index = -1;
            Func<string[], string, int> func = (array, vl) =>
            {
                index = ReturnIndex(array, vl, index + 1);
                return index;
            };

            string[] s1 = "aaabbbababaabbcccrr".ToCharArray().Select(x=>x.ToString()).ToArray();
            string[] s2 = "abcbbaddbaabbddd".ToCharArray().Select(x => x.ToString()).ToArray();
            //var ss = string.Concat(s1.Intersect(s2,new CompareClassModel()));
            var chlist1 = s1.Select(x => new KeyValuePair<int, string>(func(s1, x), x)).ToList();

            index = -1;
            var chlist2 = s2.Select(x => new KeyValuePair<int, string>(func(s2, x), x)).ToList();

            var list1 = chlist1.Intersect(chlist2, new CompareClassModel()).ToList();   //
            var list2 = s1.Intersect(s2).ToList();   //, new CompareClassModel()

            index = -1;
            var list3 = s1.Where(x => list2.Any(y => y == x)).Select(x => new KeyValuePair<int, string>(func(s1, x), x)).ToList();
            index = -1;
            var list4 = s2.Where(x => list2.Any(y => y == x)).Select(x => new KeyValuePair<int, string>(func(s2, x), x)).ToList();
        }

        public static int ReturnIndex<T>(T[] array, T vl, int start)
        {
            return Array.IndexOf(array, vl, start);
        }

        public async static Task Main2Async()
        {
            var ss = "工，工a,a bb";
            var reg = new Regex($"[\u4e00-\u9fa5]|([\\w]+)");
            var matches = reg.Matches(ss);
            var list1 = matches.Select(x => x.Value).ToList();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc = Encoding.GetEncoding("gb2312");
            var enc1 = Encoding.GetEncoding("gbk");
            var encs = Encoding.GetEncodings();
        }
        public async static Task Main1Async()
        {
            var f1 = @"C:\GitHubRepos\FileContentIndexSearch\FileSearchByIndex\FileSearchByIndex\bin\Debug\net6.0-windows\Indexes\C__GitHubRepos_FileContentIndexSearch_FileSearchByIndex.json";
            var f2 = @"C:\GitHubRepos\FileContentIndexSearch\FileSearchByIndex\FileSearchByIndex\bin\Debug\net6.0-windows\Indexes\C__GitHubRepos_FileContentIndexSearch_FileSearchByIndex1.json";

            var txt1 = await ReadFileAsync(f1);
            var txt2 = await ReadFileAsync(f2);

            var obj1 = ConversionsHelper.DeserializeJson<IndexFilesModel>(txt1);
            var obj2 = ConversionsHelper.DeserializeJson<IndexFilesModel>(txt2);
        }

        public async static Task<string> ReadFileAsync(string path)
        {
            using StreamReader reader = new StreamReader(path, true);
            return await reader.ReadToEndAsync();
        }
        public async static Task MainAsync()
        {
            string ss = "public \r\nclass CSAnalysisService \n: xxx.BaseAnalysis\"<CSAnalysisService>, IAnalysisService where T:class";
            Regex LineWrap = new($"({EnviConst.EnvironmentNewLine}|{EnviConst.SpecNewLine1})");
            Regex LineWrap1 = new($"((\r)?{EnviConst.SpecNewLine1})|[\\w\\W]+");

            var srl = LineWrap.Replace(ss, "-XXXX-");
            var srl1 = LineWrap1.Replace(ss, "-XXXX-");

            var matches = LineWrap.Matches(ss);
            var matches1 = LineWrap1.Matches(ss);
            var isEqual = srl == srl1;

            var str = "";
            foreach (Match mtch in matches1)
            {
                str += mtch.Result(mtch.Value + "/");
            }
        }
    }

    public class CompareClassModel : 
        IEqualityComparer<char>, IEqualityComparer<KeyValuePair<int, char>>, 
        IEqualityComparer<string>, IEqualityComparer<KeyValuePair<int, string>>
    {
        bool IEqualityComparer<char>.Equals(char x, char y)
        {
            return x == y;
        }

        int IEqualityComparer<char>.GetHashCode([DisallowNull] char obj)
        {
            return GetHashCode();
        }

        bool IEqualityComparer<KeyValuePair<int, char>>.Equals(KeyValuePair<int, char> x, KeyValuePair<int, char> y)
        {
            return x.Value == y.Value;
        }

        int IEqualityComparer<KeyValuePair<int, char>>.GetHashCode(KeyValuePair<int, char> obj)
        {
            return GetHashCode();
        }

        bool IEqualityComparer<string>.Equals(string? x, string? y)
        {
            return x == y;
        }

        int IEqualityComparer<string>.GetHashCode(string obj)
        {
            return GetHashCode();
        }

        bool IEqualityComparer<KeyValuePair<int, string>>.Equals(KeyValuePair<int, string> x, KeyValuePair<int, string> y)
        {
            return x.Value == y.Value;
        }

        int IEqualityComparer<KeyValuePair<int, string>>.GetHashCode(KeyValuePair<int, string> obj)
        {
            return GetHashCode();
        }
    }
}
