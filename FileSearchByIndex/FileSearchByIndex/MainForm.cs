using FileSearchByIndex.Core.Interfaces;

namespace FileSearchByIndex
{
    public partial class MainForm : Form, IForm
    {
        protected log4net.ILog _log;
        public MainForm()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
            mnMainMenu.SetParentIForm(this);
        }

        public void SetParentIForm(IForm parent)
        {
            throw new NotImplementedException();
        }
    }
}