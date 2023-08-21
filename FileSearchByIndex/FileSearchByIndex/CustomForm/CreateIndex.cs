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
