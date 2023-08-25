using ConsoleTest.TaskTests;

Console.WriteLine("Hello, World!");

{
    //Console.WriteLine($"In Entrance - Thread Id - {Thread.CurrentThread.ManagedThreadId}");
    //Console.WriteLine();
    //TaskTestSvr testSvr = new TaskTestSvr();
    //await testSvr.Test1();

    Console.WriteLine(Path.GetFileName("c:\\aa\bb\\cc.txt"));
    Console.WriteLine(Path.GetExtension("c:\\aa\bb\\cc.txt"));

    var files = Directory.GetFiles(@"D:\wiwang\Downloads", "*.*" );

    Console.WriteLine("......");
    Console.ReadLine();
}