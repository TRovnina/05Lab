using System.Windows.Controls;
using Laboratory05.ViewModel;

namespace Laboratory05.View
{
    public partial class ProcessListView : UserControl
    {
        public ProcessListView()
        {
            InitializeComponent();
            DataContext = new ProcessListViewModel();
        }
    }
}
