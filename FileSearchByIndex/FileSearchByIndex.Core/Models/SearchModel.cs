using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Models
{
    public class SearchModel
    {
        public string? Filter { get; set; }
        public bool IsIncludeSub { get; set; }
        public string IndexFileName { get; set; }
        public string IndexDescription { get; set; }
        public string SearchPath { get; set; }
    }
}
