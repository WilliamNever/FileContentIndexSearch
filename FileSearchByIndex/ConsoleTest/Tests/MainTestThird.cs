using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Helper;
using FileSearchByIndex.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleTest.Tests
{
    public class MainTestThird
    {
        public async static Task Main2Async()
        {
            var ss = "工，工a,a bb";
            var reg = new Regex($"[\u4e00-\u9fa5]|([\\w]+)");
            var matches = reg.Matches(ss);
            var list1 = matches.Select(x => x.Value).ToList();
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
}
