using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.CustomForm;

namespace FileSearchByIndex.UserControls
{
    public partial class MainMenu : UserControl, IForm
    {
        private IForm? mform = null;
        public void SetIForm(IForm parent) => mform = parent;
        public MainMenu()
        {
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
                        fmCI.SetIForm(mform!);
                        fmCI.ShowDialog();
                    }
                    break;
            }
        }
    }
}
