using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.CustomForm;

namespace FileSearchByIndex.UserControls
{
    public partial class MainMenu : UserControl
    {
        private IForm mform;
        public MainMenu(IForm mf)
        {
            mform = mf;
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
                    using(fmCreateIndex fmCI = new fmCreateIndex(mform))
                    {
                        fmCI.ShowDialog();
                    }
                    break;
            }
        }
    }
}
