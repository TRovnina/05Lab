using System.Windows.Controls;
using Laboratory05.Tools;
using Laboratory05.Tools.Navigation;
using Laboratory05.ViewModel;

namespace Laboratory05.View
{
    public partial class MainPageView : UserControl, INavigatable
    {
        private readonly BasicViewModel _mainPage;
       
        public MainPageView()
        {
           InitializeComponent();
            _mainPage = new MainPageViewModel();
            DataContext = _mainPage;
        }

        INavigatable INavigatable.Refresh()
        {
            _mainPage.Refresh();
            return this;
        }
    }
}
