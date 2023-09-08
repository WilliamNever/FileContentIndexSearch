using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Models
{
    public class ListOption
    {
        public string FullFileName { get; set; } = null!;
        public string FileName { get { return Path.GetFileName(FullFileName); } }
        public override string ToString()
        {
            return FileName;
        }
    }
}
