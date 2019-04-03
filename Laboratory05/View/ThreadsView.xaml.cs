using System.Windows.Controls;
using Laboratory05.Tools;
using Laboratory05.Tools.Navigation;
using Laboratory05.ViewModel;

namespace Laboratory05.View
{
    /// <summary>
    /// Interaction logic for ModulesView.xaml
    /// </summary>
    public partial class ThreadsView : UserControl, INavigatable
    {
        private readonly BasicViewModel _threads;



        public ThreadsView()
        {
            InitializeComponent();
            _threads = new ThreadsViewModel();
            DataContext = _threads;
        }

        INavigatable INavigatable.Refresh()
        {
            _threads.Refresh();
            return this;
        }
    }
}
