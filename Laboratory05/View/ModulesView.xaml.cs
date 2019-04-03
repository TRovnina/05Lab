using System.Windows.Controls;
using Laboratory05.Tools;
using Laboratory05.Tools.Navigation;
using Laboratory05.ViewModel;

namespace Laboratory05.View
{
    /// <summary>
    /// Interaction logic for ModulesView.xaml
    /// </summary>
    public partial class ModulesView : UserControl, INavigatable
    {
        private readonly BasicViewModel _modules;



        public ModulesView()
        {
            InitializeComponent();
            _modules = new ModulesViewModel();
            DataContext = _modules;
        }

        INavigatable INavigatable.Refresh()
        {
            _modules.Refresh();
            return this;
        }
    }
}
