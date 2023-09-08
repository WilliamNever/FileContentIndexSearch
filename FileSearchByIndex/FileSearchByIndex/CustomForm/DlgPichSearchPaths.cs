using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Models;
using System.Data;

namespace FileSearchByIndex.CustomForm
{
    public partial class DlgPichSearchPaths : Form
    {
        protected log4net.ILog _log;
        public DlgPichSearchPaths()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
        }

        public void Initial(List<ListOption>? lists)
        {
            var files = Directory.GetFiles(EnviConst.IndexesFolderPath, "*.json");
            cblPathList.Items.AddRange(files.Select(x => new ListOption { FullFileName = x }).ToArray());
            if (lists != null)
            {
                var items = cblPathList.Items.OfType<ListOption>().ToList();
                var indexes = lists.Select(x =>
                    items.FindIndex(y =>
                    y.FullFileName.Equals(x.FullFileName, StringComparison.CurrentCultureIgnoreCase)))
                    .Where(x => x > -1);
                foreach (int idx in indexes)
                    cblPathList.SetItemChecked(idx, true);
            }
        }
        public List<ListOption> GetSelectedIndexes()
        {
            return cblPathList.CheckedItems.OfType<ListOption>().ToList();
        }
    }
}
