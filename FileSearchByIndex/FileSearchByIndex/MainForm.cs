using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Models;

namespace FileSearchByIndex
{
    public partial class MainForm : BaseForm<MainForm>, IForm
    {
        public MainForm()
        {
            mnMainMenu = new UserControls.MainMenu(this);
            InitializeComponent();
        }
    }
}