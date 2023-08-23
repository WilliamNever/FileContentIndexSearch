using log4net.Core;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Helper
{
    public class Log4NetExceptionDataConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var data = loggingEvent.ExceptionObject.Data;
            if (data != null)
            {
                foreach (var key in data.Keys)
                {
                    writer.Write("Data[{0}]={1}" + Environment.NewLine, key, data[key]);
                }
            }
        }
    }
}
