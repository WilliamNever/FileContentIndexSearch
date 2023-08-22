using FileSearchByIndex.Core.Interfaces;

namespace FileSearchByIndex.CustomForm
{
    public partial class fmCreateIndex : Form, IForm
    {
        private IForm? mform = null;
        public fmCreateIndex()
        {
            InitializeComponent();
            searchSurface.SetIForm(this);
        }

        public void SetIForm(IForm parent) => mform = parent;
    }
}
