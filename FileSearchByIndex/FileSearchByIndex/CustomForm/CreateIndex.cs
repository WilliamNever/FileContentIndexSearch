using FileSearchByIndex.Core.Interfaces;
using System.Windows.Forms;

namespace FileSearchByIndex.CustomForm
{
    public partial class fmCreateIndex : Form, IForm
    {
        protected object lockObj = new object();
        protected int MessagesLimited = 1000;
        protected List<string> Messages;

        protected log4net.ILog _log;
        private IForm? mform = null;
        protected string WinTitle;
        public fmCreateIndex()
        {
            Messages = new List<string>();
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
                    AddTopNewMessage(txtInfo, $"{message ?? string.Empty}");
                    break;
            }
        }
        private void AddTopNewMessage(TextBox txtInfo, string str)
        {
            lock (lockObj)
            {
                Messages.Add(str);
                Messages = Messages.TakeLast(MessagesLimited).ToList();
                txtInfo.Text = string.Join(Environment.NewLine, Messages);
                txtInfo.SelectionStart = txtInfo.Text.Length;
                txtInfo.ScrollToCaret();
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
