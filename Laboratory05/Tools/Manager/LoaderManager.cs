using System.Windows;

namespace Laboratory05.Tools.Manager
{
    internal class LoaderManager
    {
        private static readonly object Locker = new object();
        private static LoaderManager _instance;

        internal static LoaderManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (Locker)
                {
                    return _instance ?? (_instance = new LoaderManager());
                }
            }
        }

        private ILoader _loader;

        private LoaderManager()
        {

        }

        internal void Initialize(ILoader loader)
        {
            _loader = loader;
        }

        internal void ShowLoader()
        {
            _loader.LoaderVisibility = Visibility.Visible;
            _loader.IsControlEnabled = false;
        }
        internal void HideLoader()
        {
            _loader.LoaderVisibility = Visibility.Hidden;
            _loader.IsControlEnabled = true;
        }
    }
}
