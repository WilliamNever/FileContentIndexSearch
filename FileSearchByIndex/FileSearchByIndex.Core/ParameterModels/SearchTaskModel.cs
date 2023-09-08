using FileSearchByIndex.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace FileSearchByIndex.Core.ParameterModels
{
    public class SearchTaskModel
    {
        [Required(ErrorMessage = "Search path is required")]
        [MinLength(1, ErrorMessage = "Search path is required")]
        public List<ListOption> ListOptions { get; set; } = null!;
        [Required(ErrorMessage = "Keywords are required")]
        public string Keywords { get; set; } = null!;
        [Required(ErrorMessage = "File filter is required")]
        public string FileFilters { get; set; } = null!;
        public bool IsSearchingInSampleText { get; set; } = false;
    }
}
