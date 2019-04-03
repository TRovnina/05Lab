using System.Windows;
using Laboratory05.Tools;
using Laboratory05.Tools.Manager;

namespace Laboratory05.ViewModel
{
    internal class MainWindowViewModel : BasicViewModel, ILoader
    {
        #region Fields
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        #endregion

        #region Properties
        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            LoaderManager.Instance.Initialize(this);
        }

        public override void Refresh()
        {
            
        }
    }
}
