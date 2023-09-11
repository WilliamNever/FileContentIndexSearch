using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Models;
using System.Data;

namespace FileSearchByIndex.CustomForm
{
    public partial class DlgPichSearchPaths : Form
    {
        protected log4net.ILog _log;
        protected List<ListOption>? OptionsList = null;
        public bool HasChangeSelections { get; protected set; } = false;
        public DlgPichSearchPaths()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
        }

        public void Initial(List<ListOption>? lists)
        {
            HasChangeSelections = false;
            cblPathList.Items.Clear();
            OptionsList = lists;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var list = cblPathList.CheckedItems.OfType<ListOption>().ToList();
            foreach (var idx in list)
            {
                if (File.Exists(idx.FullFileName))
                {
                    try
                    {
                        File.Delete(idx.FullFileName);
                    }
                    catch (Exception ex)
                    {
                        _log.Error($"Failed to delete file {idx.FullFileName}", ex);
                    }
                }
            }
            Initial(OptionsList);
            HasChangeSelections = true;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            HasChangeSelections = true;

            for (int i = 0; i < cblPathList.Items.Count; i++)
                cblPathList.SetItemChecked(i, true);
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            HasChangeSelections = true;

            for (int i = 0; i < cblPathList.Items.Count; i++)
                cblPathList.SetItemChecked(i, false);
        }

        private void btnInvertSelect_Click(object sender, EventArgs e)
        {
            HasChangeSelections = true;

            var selectedItem = cblPathList.CheckedItems.OfType<ListOption>();
            var items = cblPathList.Items.OfType<ListOption>().ToArray();
            var checkedIndex = selectedItem.Select(x => Array.IndexOf(items, x)).ToList();
            for (int i = 0; i < items.Length; i++)
                cblPathList.SetItemChecked(i, !checkedIndex.Any(x => x == i));
        }
    }
}
