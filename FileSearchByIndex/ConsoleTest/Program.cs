using ConsoleTest.TaskTests;
using FileSearchByIndex.Core.Consts;
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

    string ss = $"fs{EnviConst.NewLine}{EnviConst.NewLine}sf";
    var regx = new Regex($"([\\s])");
    var isMatch = regx.IsMatch(ss);
    var ms = regx.Matches(ss);
    Regex LineWrap = new($"({EnviConst.NewLine})");
    var msLw = LineWrap.Matches(ss);
    var spL = ss.Split($"{EnviConst.NewLine}");

    var ii = 0 ;
    Console.WriteLine("......");
    Console.ReadLine();
}