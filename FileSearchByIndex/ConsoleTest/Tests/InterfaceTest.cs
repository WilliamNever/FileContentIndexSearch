using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Tests
{
    public class MainSecond
    {
        public static async Task Test()
        {
            ITaskWorking<BaseClass> iftc1 = new InterfaceTestClass1();
            await iftc1.StartWorking(new BaseClass());
            ITaskWorking<ExBase>  iftc2 = iftc1;
            await iftc2.StartWorking(new ExBase());
        }
    }
    public class InterfaceTestClass1 : ITaskWorking<BaseClass>
    {
        public async Task StartWorking(BaseClass Param)
        {
            await Console.Out.WriteLineAsync(Param.Name);
        }
    }
    public class InterfaceTestClass2 : ITaskWorking<ExBase>
    {
        public async Task StartWorking(ExBase Param)
        {
            await Console.Out.WriteLineAsync(Param.Message);
        }
    }

    public class BaseClass
    {
        public string Name { get; set; }
    }
    public class ExBase : BaseClass
    {
        public string Message { get; set; }
    }

    public interface ITaskWorking<in T>
    {
        Task StartWorking(T Param);
    }
}
