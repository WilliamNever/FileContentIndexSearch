using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Models
{
    public class IndexFilesModel
    {
        public List<SingleFileIndexModel> IndexFiles { get; set; } = new List<SingleFileIndexModel>();
        public string Description { get; set; }
        public string IndexOfFolder { get; set; }
    }
}
