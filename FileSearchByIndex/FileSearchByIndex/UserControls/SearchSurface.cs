using FileSearchByIndex.Core;
using FileSearchByIndex.Core.Helper;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Infrastructure.Services;

namespace FileSearchByIndex.UserControls
{
    public partial class SearchSurface : UserControl, IForm
    {
        protected log4net.ILog _log;
        private IForm? pform = null;
        public SearchSurface()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
        }
        public void SetParentIForm(IForm parent) => pform = parent;

        private void btnBrowsPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            fbd.SelectedPath = txtPath.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = fbd.SelectedPath;
            }
        }
        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            SearchModel search = new()
            {
                Filter = txtFilter.Text,
                IsIncludeSub = cbkIncludeSub.Checked,
                SearchPath = txtPath.Text,
                IndexFileName = txtIndexFileName.Text,
                IndexDescription = txtDescription.Text
            };
            
            _ = RunAsync(search);
        }
        private async Task RunAsync(SearchModel search)
        {
            ICreateIndexService icrs = ServicesRegister.GetService<ICreateIndexService>();
            Enabled = false;
            try
            {
                Action<string> action = str => {
                    Invoke(ReFreshx, str);
                };
                var path = await Task.Run(async () => await icrs.CreateIndexFileAsync(search, action));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error - {ex.Message}");
                ex.Data.Add("Search", ConversionsHelper.SerializeToJson(search));
                _log.Error("Error in creating index file", ex);
            }
            finally
            {
                Enabled = true;
            }
        }
        public void ReFreshx(string mess)
        {
            txtDescription.Text += mess;
        }
    }
}
