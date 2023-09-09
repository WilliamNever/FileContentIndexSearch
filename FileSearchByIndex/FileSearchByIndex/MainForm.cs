using FileSearchByIndex.Core.Interfaces;
using System.Text;

namespace FileSearchByIndex
{
    public partial class MainForm : Form, IForm
    {
        protected log4net.ILog _log;
        public MainForm()
        {
            _log = log4net.LogManager.GetLogger(GetType());
            InitializeComponent();
            mnMainMenu.SetParentIForm(this);

            //var exp = new Exception("sxxx");
            //exp.Data.Add("aaa", "aValue");
            //exp.Data.Add("bbb", "bValue");
            //_log.Error("start", exp);
        }
    }
}