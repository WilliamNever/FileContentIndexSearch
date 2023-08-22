using FileSearchByIndex.Core.Interfaces;

namespace FileSearchByIndex.UserControls
{
    public partial class SearchSurface : UserControl, IForm
    {
        private IForm? pform = null;
        public SearchSurface()
        {
            InitializeComponent();
        }
        public void SetIForm(IForm parent) => pform = parent;

        private void btnBrowsPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            fbd.SelectedPath = txtPath.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = fbd.SelectedPath;
            }
        }

        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
        }
    }
}
