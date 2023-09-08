using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleTest.Tests
{
    public class MainFirst
    {
        public static void Test1()
        {
            //Console.WriteLine($"In Entrance - Thread Id - {Thread.CurrentThread.ManagedThreadId}");
            //Console.WriteLine();
            //TaskTestSvr testSvr = new TaskTestSvr();
            //await testSvr.Test1();

            //Console.WriteLine(Path.GetFileName("c:\\aa\bb\\cc.txt"));
            //Console.WriteLine(Path.GetExtension("c:\\aa\bb\\cc.txt"));

            //var files = Directory.GetFiles(@"D:\wiwang\Downloads", "*.*" );

            //string ss = $"fs{EnviConst.NewLine}{EnviConst.NewLine}sf";
            //var regx = new Regex($"([\\s])");
            //var isMatch = regx.IsMatch(ss);
            //var ms = regx.Matches(ss);
            //Regex LineWrap = new($"({EnviConst.NewLine})");
            //var msLw = LineWrap.Matches(ss);
            //var spL = ss.Split($"{EnviConst.NewLine}");

            //string ss = "public \r\nclass CSAnalysisService : xxx.BaseAnalysis<CSAnalysisService>, IAnalysisService where T:class";
            //ss += "\r\n {CSAnalysisService : BaseAnalysis<CSAnalysisService>, IAnalysisService}";
            //ss += "\r\npublic class CSAnalysisService : BaseAnalysis<CSAnalysisService>, IAnalysisService\r\n{}";
            //var regx = new Regex($"[\\w\\s]*(class){{1}}[\\w\\W]*?({{){{1}}");
            //var ms = regx.Matches(ss);

            //string ss = "namespace ProCure.MVCTests.Utilities\r\n{\r\n    public class FileStringWriter:IDisposable\r\n    {\r\n        private FileStream fs;\r\n        private StreamWriter sw;\r\n        private string Path;\r\n        public FileStringWriter(string FilePath)\r\n        {\r\n            Path = FilePath;\r\n       }\r\n        public void Open()\r\n        {\r\n            fs = new FileStream(Path, FileMode.Create);\r\n            sw = new StreamWriter(fs);\r\n        }\r\n        public void WriteFlushed(string str)\r\n        {\r\n            sw.Write(str);\r\n            Flush();\r\n        }\r\n        public void Flush()\r\n        {\r\n            sw.Flush();\r\n        }\r\n        public void Close()\r\n        {\r\n            sw.Close();\r\n            fs.Close();\r\n        }\r\n\r\n        public void Dispose()\r\n        {\r\n            Close();\r\n            sw.Dispose();\r\n            fs.Dispose();\r\n        }\r\n    }\r\n}";
            //Regex regx = new($"([\\w\\s]*(class|interface|enum|struct){{1}})[ ]+[\\w]+[\\w\\W]*?({EnviConst.EnvironmentNewLine})");
            //var ms = regx.Matches(ss);
            //var ot = regx.Replace(ss, "-");

            //string txt = "";
            //using(StreamReader sr=new StreamReader(@"D:\WQPersonal\GitsProjects\FileContentIndexSearch\FileSearchByIndex\FileSearchByIndex\bin\Debug\net6.0-windows\Indexes\WorkSpaces.DevAzure.json",true)) 
            //{
            //    txt = sr.ReadToEnd();
            //}
            //var obj = ConversionsHelper.DeserializeJson<IndexFilesModel>(txt);
            //var files = obj.IndexFiles.Where(x => x.KeyWords.Any(y => y.KeyWord.Contains("Save") && y.Frequency > 1)).ToList();


            Func<string, string, bool, string> xClearString = (txt, padString, ignoreCase) =>
            {
                var regDCom = ignoreCase ? new Regex($"{padString}", RegexOptions.IgnoreCase) : new Regex($"{padString}");
                var regString = ignoreCase ? new Regex($"{padString}[\\w\\W]*?{padString}", RegexOptions.IgnoreCase) : new Regex($"{padString}[\\w\\W]*?{padString}");
                if ((regDCom.Matches(txt).Count & 1) > 0)
                {
                    txt += padString;
                }
                var str = regString.Replace(txt, "");
                return str;
            };

            string ss = "public \r\nclass CSAnalysisService : xxx.BaseAnalysis\"<CSAnalysisService>, IAnalysisService where T:class";
            var res = ClearString(ss, "\"", true);
        }

        public static string ClearString(string txt, string regExpString, bool ignoreCase = true, string? appendString = null)
        {
            var regDCom = ignoreCase ? new Regex($"{regExpString}", RegexOptions.IgnoreCase)
                : new Regex($"{regExpString}");
            var regString = ignoreCase ? new Regex($"{regExpString}[\\w\\W]*?{regExpString}", RegexOptions.IgnoreCase)
                : new Regex($"{regExpString}[\\w\\W]*?{regExpString}");
            if ((regDCom.Matches(txt).Count & 1) > 0)
            {
                txt += appendString == null ? regExpString : appendString;
            }
            var str = regString.Replace(txt, "");
            return str;
        }
    }
}
