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
    }
}
