using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Core.ParameterModels;
using FileSearchByIndex.Infrastructure;

namespace FileSearchByIndex.CustomForm
{
    public partial class SearchResultForm : Form, IForm, ITaskWorking<SearchTaskModel>
    {
        protected log4net.ILog _log = null!;
        public CancellationTokenSource _cts { get; protected set; } = null!;
        protected Task RunningTask { get; set; } = null!;
        protected SearchTaskModel _searchModel { get; set; } = null!;

        protected string WinTitle;
        public SearchResultForm()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
            WinTitle = Text;
            tsStatus.Text = "Prepare to Search ...";
        }


        public async Task CancelWorkingAsync(string workName = "NoName")
        {
            _cts?.Cancel();
            if (RunningTask != null && RunningTask.Status != TaskStatus.RanToCompletion)
                Invoke(AcceptMessage, $"Current status is {RunningTask.Status} - Begin trying to Cancel the task....", "");
        }

        public Task StartWorkingAsync(SearchTaskModel Param, CancellationTokenSource? cts = null)
        {
            _searchModel = Param;
            _cts = cts ?? new CancellationTokenSource();
            RunningTask = StartSearchingAsync(Param, _cts);
            return RunningTask;
        }

        private async Task StartSearchingAsync(SearchTaskModel Param, CancellationTokenSource? cts = null)
        {
            Invoke(AcceptMessage, $"Start to Search ...", "start");

            var searching = ServicesRegister.GetService<ISearchingIndexFilesSerivce>();

            Invoke(AcceptMessage, $"Searching is finished ...", "stop");
        }

        private void fileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "ExportResults":
                    break;
                default:
                    break;
            }
        }

        private void processToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "cancel":
                    _ = CancelWorkingAsync();
                    break;
                case "clear":
                    break;
                case "research":
                    _cts = new CancellationTokenSource();
                    RunningTask = StartSearchingAsync(_searchModel, _cts);
                    break;
                default: break;
            }
        }
        public void AcceptMessage(string mess, string location = "")
        {
            tsStatus.Text = mess;

            switch (location)
            {
                case "start":
                    reSearchToolStripMenuItem.Enabled = false;
                    clearResultsToolStripMenuItem.Enabled = false;
                    break;

                case "stop":
                    reSearchToolStripMenuItem.Enabled = true;
                    clearResultsToolStripMenuItem.Enabled = true;
                    break;

                default: break;
            }
        }
    }
}
