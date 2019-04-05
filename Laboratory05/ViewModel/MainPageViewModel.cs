using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Laboratory05.Tools;
using Laboratory05.Tools.Manager;
using Laboratory05.Tools.Navigation;

namespace Laboratory05.ViewModel
{
    internal class MainPageViewModel: BasicViewModel
    {
        private ICommand _getModulesCommand;
        private ICommand _getThreadsCommand;
        private ICommand _stopProcessCommand;
        private ICommand _openFolderCommand;
        private ICommand _sortCommand;
        
        

        public ICommand GetModulesCommand
        {
            get { return _getModulesCommand ?? (_getModulesCommand = new RelayCommand<object>(GetModulesImplementation)); }
        }

        private void GetModulesImplementation(object obj)
        {
            if (!IsSelected()) return;

            NavigationManager.Instance.Navigate(ViewType.Modules);
        }

        public ICommand OpenFolderCommand
        {
            get { return _openFolderCommand ?? (_openFolderCommand = new RelayCommand<object>(OpenFolderImplementation)); }
        }

        private void OpenFolderImplementation(object obj)
        {
            if (!IsSelected()) return;
            string path = StationManager.CurrentProcess.Path;

            if (path.Equals(""))
                Process.Start("c:\\");
            else
            {
                int end = path.LastIndexOf("\\", StringComparison.Ordinal);
                Process.Start(path.Substring(0, end));
            }

        }

        public ICommand GetThreadsCommand
        {
            get { return _getThreadsCommand ?? (_getThreadsCommand = new RelayCommand<object>(GetThreadsImplementation)); }
        }

        private void GetThreadsImplementation(object obj)
        {
            if (!IsSelected()) return;

            NavigationManager.Instance.Navigate(ViewType.Threads);
        }


        public ICommand SortCommand
        {
            get { return _sortCommand ?? (_sortCommand = new RelayCommand<object>(SortImplementation)); }
        }
        
        private void SortImplementation(object obj)
        {
            StationManager.DataStorage.SortList(obj.ToString());
            Refresh();
        }

        public ICommand StopProcessCommand
        {
            get { return _stopProcessCommand ?? (_stopProcessCommand = new RelayCommand<object>(StopProcessImplementation)); }
        }

        private void StopProcessImplementation(object obj)
        {
            if (!IsSelected()) return;

            Process.GetProcessById(StationManager.CurrentProcess.Id).Kill();
            Refresh();
        }

        private bool IsSelected()
        {
            if (StationManager.CurrentProcess == null)
            {
                MessageBox.Show("Select something!");
                return false;
            }

            return true;
        }

       
        public override void Refresh()
        {
        }

    }
}
