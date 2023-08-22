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
            searchSurface.SetIForm(this);
        }

        public void SetIForm(IForm parent) => mform = parent;
    }
}
