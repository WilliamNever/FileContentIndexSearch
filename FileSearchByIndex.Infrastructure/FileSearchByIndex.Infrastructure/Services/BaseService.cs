using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Infrastructure.Services
{
    public class BaseService<T> where T : class
    {
        protected log4net.ILog _log = log4net.LogManager.GetLogger(typeof(T));
    }
}
