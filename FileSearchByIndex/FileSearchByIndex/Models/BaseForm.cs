using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Models
{
    public class BaseForm<T> : Form where T : Form
    {
        protected log4net.ILog _log = log4net.LogManager.GetLogger(typeof(T));
    }
}
