using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Models
{
    public class SearchModel
    {
        public string? Filter { get; set; }
        public bool IsIncludeSub { get; set; }
        [Required(ErrorMessage = "Index file name is required")]
        public string IndexFileName { get; set; } = null!;
        public string IndexFileFullName { get; set; } = null!;
        public string? IndexDescription { get; set; }
        [Required(ErrorMessage = "Search path is required")]
        public string SearchPath { get; set; } = null!;
    }
}
