using FileSearchByIndex.Core;
using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Helper;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;

namespace FileSearchByIndex.UserControls
{
    public partial class SearchSurface : UserControl, IForm
    {
        protected log4net.ILog _log;
        private IForm? pform = null;
        protected CancellationTokenSource _cts;
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
                IndexFileFullName = Path.Combine(EnviConst.IndexesFolderPath, txtIndexFileName.Text),
                IndexDescription = txtDescription.Text
            };
            var rsl = search.SimpleValidateProperties();
            if (rsl.Count > 0)
            {
                MessageBox.Show(this, " - " + string.Join($"{EnviConst.NewLine} - ", rsl.Select(x => x.ErrorMessage)), "Validate Result -");
                return;
            }
            pform?.CleanMessages();
            AcceptMessage($"Beginning Searching - {Environment.NewLine}");
            AcceptMessage($"{ConversionsHelper.SerializeToFormattedJson(search)}{Environment.NewLine}{Environment.NewLine}");
            _ = RunAsync(search);
        }
        private async Task RunAsync(SearchModel search)
        {
            Enabled = false;
            ICreateIndexService icrs = ServicesRegister.GetService<ICreateIndexService>();
            Action<string> action = str =>
            {
                Invoke(AcceptMessage, str);
            };
            try
            {
                _cts = new CancellationTokenSource();
                var IndexFileFullName = await Task.Run(async () => await icrs.CreateIndexFileAsync(search, action, _cts.Token), _cts.Token);
                action.Invoke($"{Environment.NewLine}{Environment.NewLine} Task finished -");
                action.Invoke($"Index file was created - {IndexFileFullName} -");
            }
            catch (Exception ex)
            {
                ex.Data.Add("Search", ConversionsHelper.SerializeToFormattedJson(search));
                _log.Error("Error in creating index file", ex);
                action.Invoke(ex.Message);
            }
            finally
            {
                Enabled = true;
            }
        }
        public void AcceptMessage(string mess)
        {
            pform?.AcceptMessage(mess);
        }
        public virtual void CancelWorking(string workName = "NoName")
        {
            _cts?.Cancel();
        }
    }
}
