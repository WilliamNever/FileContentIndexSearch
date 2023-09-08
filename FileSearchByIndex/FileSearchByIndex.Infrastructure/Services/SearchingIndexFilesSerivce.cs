using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Services;
using FileSearchByIndex.Core.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Infrastructure.Services
{
    public class SearchingIndexFilesSerivce : BaseService<SearchingIndexFilesSerivce>, ISearchingIndexFilesSerivce
    {
        protected TaskThreadSettings _taskSettings = null!;
        public SearchingIndexFilesSerivce(IOptions<TaskThreadSettings> TaskSettings)
        {
            _taskSettings = TaskSettings.Value;
        }
    }
}
