using ConsoleTest.TaskTests;
using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Services;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

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

    string ss = "public \r\nclass CSAnalysisService : xxx.BaseAnalysis<CSAnalysisService>, IAnalysisService where T:class";
    ss += "\r\n {CSAnalysisService : BaseAnalysis<CSAnalysisService>, IAnalysisService}";
    ss += "\r\npublic class CSAnalysisService : BaseAnalysis<CSAnalysisService>, IAnalysisService\r\n{}";
    var regx = new Regex($"[\\w\\s]*(class){{1}}[\\w\\W]*?({{){{1}}");
    var ms = regx.Matches(ss);


    var ii = 0 ;
    Console.WriteLine("......");
    Console.ReadLine();
}

class aa { }