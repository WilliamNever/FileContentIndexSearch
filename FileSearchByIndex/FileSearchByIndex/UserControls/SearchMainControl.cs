using FileSearchByIndex.CustomForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSearchByIndex.UserControls
{
    public partial class SearchMainControl : UserControl
    {
        protected log4net.ILog _log = null!;
        public SearchMainControl()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
        }

        private void btnPickPaths_Click(object sender, EventArgs e)
        {
            DlgPichSearchPaths dlg = new DlgPichSearchPaths();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var SearchResultForm = new SearchResultForm();
            SearchResultForm.Show();
            _ = SearchResultForm.StartWorkingAsync(new Core.ParameterModels.SearchTaskModel());

        }
    }
}
