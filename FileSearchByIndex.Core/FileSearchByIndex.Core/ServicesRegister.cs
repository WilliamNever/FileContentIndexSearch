using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core
{
    public static class ServicesRegister
    {
        public static IServiceCollection Services { get; set; } = new ServiceCollection();
        public static IServiceProvider Provider { get; set; }

        public static void Build()
        {
            Provider = Services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return Provider.GetService<T>() ?? throw new MissingMemberException($"Service {typeof(T).Name} does not exist.");
        }
    }
}
