using FileSearchByIndex.CustomForm;
using FileSearchByIndex.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
