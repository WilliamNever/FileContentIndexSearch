using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.CustomForm;
using FileSearchByIndex.Core.Helper;

namespace FileSearchByIndex.UserControls
{
    public partial class SearchMainControl : UserControl
    {
        protected log4net.ILog _log = null!;
        protected List<ListOption>? Selections = null;
        public SearchMainControl()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
        }

        private void btnPickPaths_Click(object sender, EventArgs e)
        {
            DlgPichSearchPaths dlg = new DlgPichSearchPaths();
            dlg.Initial(Selections);
            if (dlg.ShowDialog() == DialogResult.OK || dlg.HasChangeSelections)
            {
                Selections = dlg.GetSelectedIndexes();
                ListSearchingPath.Items.Clear();
                ListSearchingPath.Items.AddRange(Selections.ToArray());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var searchModel = new Core.ParameterModels.SearchTaskModel()
            {
                FileFilters = txtFileFilter.Text,
                IsSearchingInSampleText = chbIsSearchingInSampleTxt.Checked,
                Keywords = txtKeywords.Text,
                ListOptions = Selections!
            };
            var rsl = searchModel.SimpleValidateProperties();
            if (rsl.Count > 0)
            {
                MessageBox.Show(this, " - " + string.Join($"{EnviConst.EnvironmentNewLine} - ", rsl.Select(x => x.ErrorMessage)), "Validate Result -");
                return;
            }

            var SearchResultForm = new SearchResultForm();
            SearchResultForm.Show();
            _ = SearchResultForm.StartWorkingAsync(searchModel);

        }
    }
}
