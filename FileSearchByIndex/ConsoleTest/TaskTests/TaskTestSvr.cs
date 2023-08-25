using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.TaskTests
{
    public class TaskTestSvr
    {
        public async Task GetOne(string str)
        {
            //Console.WriteLine($"In Method - Thread Id - {Thread.CurrentThread.ManagedThreadId} - {str}");
            await Console.Out.WriteLineAsync($"In async GetOne - Thread Id - {Thread.CurrentThread.ManagedThreadId} - {str}");
            for (int i = 0; i < 100; i++)
            {
                await ConsoleWrite(str);
            }
        }
        public async Task GetTwo(string str)
        {
            //Console.WriteLine($"In Method - Thread Id - {Thread.CurrentThread.ManagedThreadId} - {str}");
            await Console.Out.WriteLineAsync($"In async GetTwo - Thread Id - {Thread.CurrentThread.ManagedThreadId} - {str}");
            for (int i = 0; i < 100; i++)
            {
                await ConsoleWrite(str);
            }
        }

        public async Task ConsoleWrite(string str)
        {
            for (int i = 0; i < 10; i++)
            {
                await Console.Out.WriteLineAsync($"In async ConsoleWrite - Thread Id - {Thread.CurrentThread.ManagedThreadId} - {str}");
            }
        }

        public async Task Test1()
        {
            Console.WriteLine($"In Test1 - Thread Id - {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine();
             Task.Run(async () => { 
            await GetOne("one");
            });
             //Task.Run(async () => {});
                 await Console.Out.WriteLineAsync($"In async entrance - Thread Id - {Thread.CurrentThread.ManagedThreadId}");
                 GetTwo("two");
            
        }
    }
}
