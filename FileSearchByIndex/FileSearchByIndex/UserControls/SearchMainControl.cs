using FileSearchByIndex.CustomForm;
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
    public partial class SearchMainControl : UserControl
    {
        public SearchMainControl()
        {
            InitializeComponent();
        }

        private void btnPickPaths_Click(object sender, EventArgs e)
        {
            DlgPichSearchPaths dlg = new DlgPichSearchPaths();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}
