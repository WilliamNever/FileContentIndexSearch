using FileSearchByIndex.Core.Interfaces;

namespace FileSearchByIndex.CustomForm
{
    public partial class fmCreateIndex : Form, IForm
    {
        protected log4net.ILog _log;
        private IForm? mform = null;
        protected string WinTitle;
        public fmCreateIndex()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
            WinTitle = Text;
            searchSurface.SetParentIForm(this);
        }

        public void SetParentIForm(IForm parent) => mform = parent;

        public virtual void CleanMessages()
        {
            txtInfo.Text = string.Empty;
        }
        public virtual void AcceptMessage(string message, string location = "")
        {
            switch (location)
            {
                case "title":
                    Text = $"{WinTitle} - {message}";
                    break;
                default:
                    txtInfo.Text += $"{message ?? string.Empty}{Environment.NewLine}";
                    txtInfo.SelectionStart = txtInfo.Text.Length;
                    txtInfo.ScrollToCaret();
                    break;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            searchSurface.CancelWorking();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInfo.Text = "";
        }
    }
}
