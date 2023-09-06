using FileSearchByIndex.Core.Interfaces;

namespace FileSearchByIndex.CustomForm
{
    public partial class fmCreateIndex : Form, IForm
    {
        protected log4net.ILog _log;
        private IForm? mform = null;
        public fmCreateIndex()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
            searchSurface.SetParentIForm(this);
        }

        public void SetParentIForm(IForm parent) => mform = parent;

        public virtual void CleanMessages()
        {
            txtInfo.Text = string.Empty;
        }
        public virtual void AcceptMessage(string message)
        {
            txtInfo.Text += $"{message ?? string.Empty}{Environment.NewLine}";
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
