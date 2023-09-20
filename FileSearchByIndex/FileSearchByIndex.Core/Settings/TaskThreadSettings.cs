using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Settings
{
    public class TaskThreadSettings : IOptions<TaskThreadSettings>
    {
        public int TaskMinCount { get; set; }
        public int TaskMaxCount { get; set; }
        public int TaskInitCount { get; set; }

        public TaskThreadSettings Value => this;
    }
}
