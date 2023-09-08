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
    public partial class DlgPichSearchPaths : Form
    {
        protected log4net.ILog _log;
        public DlgPichSearchPaths()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
        }
    }
}
