using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Helper;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Core.Settings;
using FileSearchByIndex.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleTest.Tests
{
    public class MainTestThird
    {
        public static async Task Main6Async()
        {
            var f1 = @"C:\tAnsly\SqlServer.txt";
            var txt1 = await ReadFileAsync(f1);
            var Txt_AddFirstLine = $"{EnviConst.EnvironmentNewLine}{txt1}{EnviConst.EnvironmentNewLine}";
            Regex regex = new Regex($"(?<=(\r|{EnviConst.SpecNewLine1}){{1}})((.+(block in T-sql)+.*)+?\\1)");
            //((\r)?({EnviConst.SpecNewLine1}))
            var matches = regex.Matches(Txt_AddFirstLine);

            var stxt = "\n\nTry / Catch block in T-sql\n.there is no finally block in T-sql\n";
            var matches1 = regex.Matches(stxt);
            var regGrp1 = new Regex(@"\b(?<word>[\w ]{2,})([\w\s]*?)(\k<word>)\b");
            var matches2 = regGrp1.Matches(stxt);
        }
        public static async Task Main6_4Async()
        {
            await Task.Run(() => { Console.WriteLine("in upper task"); return 1; }).ContinueWith(async xx =>
            {
                var rt = await xx;
                Console.WriteLine($"in a continue task - {rt}");
            });
        }
        public static async Task Main6_3Async()
        {
            System.Threading.AutoResetEvent autoReset = new AutoResetEvent(false);
            try
            {
                using (var tmain = Sub6_Async())
                {
                    await tmain;
                    autoReset.WaitOne(10000);
                }
            }
            catch (OperationCanceledException oex)
            { 
            }
            catch (Exception ex)
            {
            }

            string ss = "\r\nThe information provided in the Microsoft Knowledge Base is provided \"as is\" without warranty of any kind.  Microsoft disclaims all warranties, either express or implied, including the warranties of merchantability and fitness for a particular purpose.  In no Event shall Microsoft corporation or its suppliers be liable for Any damages whatsoever including direct, indirect, incidental, consequential, loss of business profits or special damages, even if Microsoft corporation or its suppliers have been advised of the possibility of such damages.  Some states do not allow the exclusion or limitation of liability for consequential or incidental damages so the foregoing limitation may not apply.\r\n\r\nMicrosoft does not warrant that the functions for the licensed software or code contained in the Knowledge Base will meet your requirements, or that the operation of the licensed software or code will be uninterrupted or error-free, or that defects in the licensed software or code can be corrected.  Furthermore, Microsoft does not warrant or make any representations regarding the use or the results of the use of the licensed software, code or related documentation in terms of their correctness, accuracy, reliability, or otherwise.  No verbal or written information or advice given by Microsoft or its authorized representatives shall create a warranty or in any way increase the scope of this warranty.  Should the licensed software or code prove defective after Microsoft has delivered the same, you, and you alone, shall assume the entire cost associated with all necessary servicing, repair or correction.\r\n";
            var reg = new Regex($"((\r)?{EnviConst.SpecNewLine1})(.+(licensed)+.+)+?\\1");
            var ms = reg.Matches(ss);
        }
        public static async Task Sub6_Async()
        {
            Thread.CurrentThread.IsBackground = false;
            
            await Task.Run(async () =>
             {
                 Thread thr=null;
                 Thread.CurrentThread.IsBackground = false;
                 CancellationTokenSource srct = new CancellationTokenSource();

                 var tsub = Task.Run(async () =>
                 {
                     thr = Thread.CurrentThread;
                     Thread.CurrentThread.IsBackground = true;
                     try
                     {
                         while (true)
                         {
                             Console.WriteLine($"Sub - {Thread.CurrentThread.ManagedThreadId} - {DateTime.Now}");
                             //Thread.Sleep(1000);
                             await Task.Delay(1000);
                             srct.Token.ThrowIfCancellationRequested();
                         }
                     }
                     catch (Exception ex)
                     {
                         throw ex;
                     }
                 }, srct.Token);
                 //srct.CancelAfter(2000);
                 await Task.Delay(2000);
                 srct.Token.Register(() => { throw new NotImplementedException("NONONONO ---"); });
                 srct.Token.Register(() => { throw new NotImplementedException("NO1 ---"); });
                 srct.Cancel(false);
                 Thread.Sleep(3000);
                 Console.WriteLine($"UP - {Thread.CurrentThread.ManagedThreadId} - {DateTime.Now}");
                 await tsub;
                 Thread.CurrentThread.IsBackground = false;

                 //thr?.Interrupt();
                 //tsub.Dispose();

                 //throw new TaskCanceledException("xxxxxx");


                 //var st = new Thread(new ThreadStart(async () =>
                 //{
                 //    try
                 //    {
                 //        await Task.Run(() =>
                 //            {
                 //                while (true)
                 //                {
                 //                    Console.WriteLine($"Sub - {Thread.CurrentThread} - {DateTime.Now}");
                 //                    Thread.Sleep(1000);
                 //                }
                 //            });
                 //    }
                 //    catch (Exception esd)
                 //    { 
                 //    }
                 //}));
                 //try
                 //{
                 //    st.Start();
                 //    Thread.Sleep(5000);

                 //    st.Interrupt();
                 //}
                 //catch (Exception emx)
                 //{
                 //}
             });
        }
        public static async Task Main6_2Async()
        {
            var txt = await ReadFileAsync(@"D:\WQPersonal\GitsProjects\FileContentIndexSearch\FileSearchByIndex\ConsoleTest\bin\Debug\net6.0\tmp\files.txt");
            var fList = ConversionsHelper.DeserializeJson<List<string>>(txt);

            CreateIndexService sc = new CreateIndexService(new TaskThreadSettings(), new AppSettings(), null);
            var files = sc.SearchDirectories(new string[] { "D:\\WorkSpaces" }, new SearchModel { Filter = "*.cs|*.txt", IsIncludeSub = true }).OrderBy(x=>x).ToList();

            var extra = files.Except(fList).ToList();
        }
        public static async Task Main6_1Async()
        {
            List<string> fileslist = new List<string>();
            var files = Directory.GetFiles("D:\\WQPersonal\\PrvCustomerTools\\FileSearchByIndex\\TempWorking", "*.json_cn");
            foreach(var file in files) {
                var txt = await ReadFileAsync(file);
                var obj1 = ConversionsHelper.DeserializeJson<SingleFileIndexModel>(txt);
                fileslist.Add(obj1!.FileFullName);
            }
            var sortedFileNames = fileslist.OrderBy(x => x).ToList();
            await WriteFileFileAsync("tmp", "files.txt", ConversionsHelper.SerializeToJson(sortedFileNames, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }
        public async static Task WriteFileFileAsync(string path, string filename, string content)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            using StreamWriter writer = new StreamWriter(Path.Combine(path, filename), false, Encoding.UTF8);
            await writer.WriteAsync(content);
            writer.Flush();
        }

        public static async Task Main5Async()
        {
            Regex rex = new Regex("(\r\n)");
            var tstring = "\r\n";
            var im = rex.IsMatch(tstring);


            string fs = "*.txt";
            string ext = Path.GetExtension(fs);

            //string fp = @"D:\WQPersonal\GitsProjects\FileContentIndexSearch\FileSearchByIndex\FileSearchByIndex\bin\Debug\net6.0-windows\Indexes\aat.json";
            //string fullPath = Path.GetDirectoryName(fp);
            //var txt1 = await ReadFileAsync(fp);
            //var obj1 = ConversionsHelper.DeserializeJson<IndexFilesModel>(txt1);
        }
        public static async Task Main4Async()
        {
            var txt = @"穿越之韬光养，,：；’‘’“”《》？、。晦完结——！";

            //Regex reg = new(@"(?<word>\b([\\p{Han}\d_-]){2,}|\b[\w -]{5,})(.*?)(\k<word>)");
            //Regex reg = new(@"[\u3400-\u4dbf\u4e00-\u9fff\uf900-\ufaff\u20000-\u3134a]");
            //Regex reg = new(@"[\uff00-\uffee]");
            //Regex reg = new(@"[\uf900-\ufaff]");
            Regex reg = new(@"[\u4e00-\u9fff\uff00-\uffee\u20000-\u3134a]");
            var ms = reg.Matches(txt, 2);
        }
        public async static Task Main3Async()
        {
            int index = -1;
            Func<string[], string, int> func = (array, vl) =>
            {
                index = ReturnIndex(array, vl, index + 1);
                return index;
            };
            string b1 = "This is the is is the on is world big is world.";
            string b2 = "abccabbddcbaabbddd";
            string b3 = "匹配由26个，英文字母的大写组成的配字符串由26个，英文大写组成写组成的字符串由" + b1;
            string[] s1 = b1.ToCharArray().Select(x=>x.ToString()).ToArray();
            string[] s2 = b2.ToCharArray().Select(x => x.ToString()).ToArray();
            //var ss = string.Concat(s1.Intersect(s2,new CompareClassModel()));
            var chlist1 = s1.Select(x => new KeyValuePair<int, string>(func(s1, x), x)).ToList();

            index = -1;
            var chlist2 = s2.Select(x => new KeyValuePair<int, string>(func(s2, x), x)).ToList();

            var list1 = chlist1.Intersect(chlist2, new CompareClassModel()).ToList();   //
            var list2 = s1.Intersect(s2, new CompareClassModel()).ToList();   //

            index = -1;
            var list3 = s1.Where(x => list2.Any(y => y == x)).Select(x => new KeyValuePair<int, string>(func(s1, x), x)).ToList();
            index = -1;
            var list4 = s2.Where(x => list2.Any(y => y == x)).Select(x => new KeyValuePair<int, string>(func(s2, x), x)).ToList();

            var regGrp1 = new Regex(@"\b(?<word>[\w ]{2,})([\w\s]*?)(\k<word>)\b");
            var grps1 = regGrp1.Matches(b1);

            var regGrp2 = new Regex(@"\b(?'word'[\w ]{2,})([\w\W]*?)(\k'word')\b");
            var grps2 = regGrp2.Matches(b1);

            var regGrp4 = new Regex(@"(?<word>[\u4e00-\u9fa5\d_]{2,}|\b[\w -]{5,})(.*?)(\k<word>)");
            var grps4 = regGrp4.Matches(b3);

            Regex regGrp3 = new Regex(@"\b[^\s]([\w -]{5,})(?:[\w\W]*?)\1");
            var grps3 = regGrp3.Matches(b1);
            var str = grps3[0].Value.Trim();
            var half = str.Length / 2;
            string rsl = "";

            for (int i = half; i > 0; i--)
            {
                var sssx = str[0..i];
                var ffxx = str[^i..];
                if (str[0..i] == str[^i..])
                {
                    rsl = str[0..i];
                    break;
                }
            }
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
            return obj.GetHashCode();
        }

        bool IEqualityComparer<KeyValuePair<int, string>>.Equals(KeyValuePair<int, string> x, KeyValuePair<int, string> y)
        {
            return x.Value == y.Value;
        }

        int IEqualityComparer<KeyValuePair<int, string>>.GetHashCode(KeyValuePair<int, string> obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
