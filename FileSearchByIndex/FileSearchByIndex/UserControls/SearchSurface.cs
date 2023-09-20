using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Helper;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.Models;
using FileSearchByIndex.Infrastructure;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.UserControls
{
    public partial class SearchSurface : UserControl, IForm
    {
        protected log4net.ILog _log = null!;
        private IForm? pform = null;
        protected CancellationTokenSource _cts = null!;
        protected Task RunningTask = null!;
        public SearchSurface()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();

            txtPath.Text = @"c:\tAnsly";
            txtIndexFileName.Text = "aat";
            txtFilter.Text = "*.txt";
            cbkIncludeSub.Checked = false;
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
                txtIndexFileName.Text = Regex.Replace(fbd.SelectedPath, "[\\W\\s]", "_");
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
                IndexFileFullName = Path.Combine(EnviConst.IndexesFolderPath, $"{txtIndexFileName.Text}.json"),
                IndexDescription = txtDescription.Text,
            };
            var rsl = search.SimpleValidateProperties();
            if (rsl.Count > 0)
            {
                MessageBox.Show(this, " - " + string.Join($"{EnviConst.EnvironmentNewLine} - ", rsl.Select(x => x.ErrorMessage)), "Validate Result -");
                return;
            }
            pform?.CleanMessages();
            AcceptMessage($"Beginning Searching - {DateTime.Now} - {Environment.NewLine}");
            AcceptMessage(txtPath.Text, "title");
            AcceptMessage($"{ConversionsHelper.SerializeToFormattedJson(search)}{Environment.NewLine}{Environment.NewLine}");
            RunningTask = RunAsync(search);
        }
        private async Task RunAsync(SearchModel search)
        {
            Enabled = false;
            ICreateIndexService icrs = ServicesRegister.GetService<ICreateIndexService>();
            Action<string> action = str =>
            {
                Invoke(AcceptMessage, str, "");
            };
            try
            {
                _cts = new CancellationTokenSource();
                var IndexFileFullName = await Task.Run(async () => await icrs.CreateIndexFileAsync(search, action, _cts.Token), _cts.Token);
                action.Invoke($"{Environment.NewLine}{Environment.NewLine} Task finished - {DateTime.Now} -");
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
                Invoke(AcceptMessage, "", "enable");
            }
        }
        public void AcceptMessage(string mess, string location = "")
        {
            switch (location)
            {
                case "enable":
                    Enabled = true;
                    break;
                default:
                    pform?.AcceptMessage(mess, location);
                    break;
            }
        }
        public virtual void CancelWorking(string workName = "NoName")
        {
            _cts?.Cancel();
            if (RunningTask != null && RunningTask.Status != TaskStatus.RanToCompletion)
                Invoke(AcceptMessage, $"Current status is {RunningTask.Status} - Begin trying to Cancel the task.... [${DateTime.Now}]", "");
        }
    }
}
