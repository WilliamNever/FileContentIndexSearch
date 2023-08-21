using FileSearchByIndex.Interfaces;

namespace FileSearchByIndex
{
    public partial class MainForm : Form, IForm
    {
        public MainForm()
        {
            mnMainMenu = new UserControls.MainMenu(this);

            InitializeComponent();
        }
    }
}