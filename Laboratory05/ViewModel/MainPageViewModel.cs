using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Laboratory05.Models;
using Laboratory05.Tools;
using Laboratory05.Tools.Manager;
using Laboratory05.Tools.Navigation;

namespace Laboratory05.ViewModel
{
    internal class MainPageViewModel: BasicViewModel
    {
        //private ObservableCollection<SystemProcess> _processes;
       // private SystemProcess _selectedProcess;

        private ICommand _getModulesCommand;
        private ICommand _getThreadsCommand;
        private ICommand _stopProcessCommand;
        private ICommand _openFolderCommand;
        private ICommand _sortCommand;

        //public ObservableCollection<SystemProcess> Processes
        //{
        //    get { return _processes; }
        //    set
        //    {
        //        _processes = value;
        //        OnPropertyChanged();
        //    }
        //}

        public ICommand GetModulesCommand
        {
            get { return _getModulesCommand ?? (_getModulesCommand = new RelayCommand<object>(GetModulesImplementation)); }
        }

        private void GetModulesImplementation(object obj)
        {
            if (StationManager.CurrentProcess == null)
            {
                MessageBox.Show("Select something!");
                return;
            }

           NavigationManager.Instance.Navigate(ViewType.Modules);
           // Refresh();
        }

        public ICommand OpenFolderCommand
        {
            get { return _openFolderCommand ?? (_openFolderCommand = new RelayCommand<object>(OpenFolderImplementation)); }
        }

        private void OpenFolderImplementation(object obj)
        {
            if (StationManager.CurrentProcess == null)
            {
                MessageBox.Show("Select something!");
                return;
            }

            int end = StationManager.CurrentProcess.Path.LastIndexOf("/");
            Process.Start(StationManager.CurrentProcess.Path.Substring(0, end));
            // Refresh();
        }

        public ICommand GetThreadsCommand
        {
            get { return _getThreadsCommand ?? (_getThreadsCommand = new RelayCommand<object>(GetThreadsImplementation)); }
        }

        private void GetThreadsImplementation(object obj)
        {
            if (StationManager.CurrentProcess == null)
            {
                MessageBox.Show("Select something!");
                return;
            }

            NavigationManager.Instance.Navigate(ViewType.Threads);
            // Refresh();
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
            if (StationManager.CurrentProcess == null)
            {
                MessageBox.Show("Select something!");
                return;
            }
            Process.GetProcessById(StationManager.CurrentProcess.Id).Close();
            Refresh();
        }


        internal MainPageViewModel()
        {
           //_processes = new ObservableCollection<SystemProcess>(StationManager.DataStorage.ProcessList);
        }

        public override void Refresh()
        {
           // Processes = new ObservableCollection<SystemProcess>(StationManager.DataStorage.ProcessList);
        }
        
    }
}
