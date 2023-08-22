using FileSearchByIndex.Core.Interfaces;

namespace FileSearchByIndex.CustomForm
{
    public partial class fmCreateIndex : Form, IForm
    {
        private IForm mform;
        public fmCreateIndex(IForm fm)
        {
            mform = fm;
            InitializeComponent();
            searchSurface.SetIForm(this);
        }
    }
}
