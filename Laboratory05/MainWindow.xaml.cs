using System.Windows;
using System.Windows.Controls;
using Laboratory05.Tools.DataStorage;
using Laboratory05.Tools.Manager;
using Laboratory05.Tools.Navigation;
using Laboratory05.ViewModel;

namespace Laboratory05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContent
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            StationManager.Initialize(new Storage());
            NavigationManager.Instance.Initialize(new NavigationInitialization(this));
            NavigationManager.Instance.Navigate(ViewType.MainPage);
        }
    }
}
