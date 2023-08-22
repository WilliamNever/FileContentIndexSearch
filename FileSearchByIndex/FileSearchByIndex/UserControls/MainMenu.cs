using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.CustomForm;

namespace FileSearchByIndex.UserControls
{
    public partial class MainMenu : UserControl, IForm
    {
        protected log4net.ILog _log;
        private IForm? mform = null;
        public void SetParentIForm(IForm parent) => mform = parent;
        public MainMenu()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
        }

        private void FileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "Save":
                    MessageBox.Show(e.ClickedItem.AccessibleName);
                    break;
                case "SaveAs":
                    MessageBox.Show(e.ClickedItem.AccessibleName);
                    break;
            }
        }

        private void indexesToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.AccessibleName)
            {
                case "CreateIndexes":
                    using(fmCreateIndex fmCI = new fmCreateIndex())
                    {
                        fmCI.SetParentIForm(mform!);
                        fmCI.ShowDialog();
                    }
                    break;
            }
        }
    }
}
