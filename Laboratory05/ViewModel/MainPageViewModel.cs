using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
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
        private ICommand _getModulesCommand;
        private ICommand _getThreadsCommand;
        private ICommand _stopProcessCommand;
        private ICommand _openFolderCommand;
        private ICommand _sortCommand;


        private ObservableCollection<SystemProcess> _processes;
        private Thread _workingThread;
        private Thread _refreshingThread;
        private readonly CancellationToken _token;
        private readonly CancellationTokenSource _tokenSource;
        
        

        public MainPageViewModel()
        {
            _processes = new ObservableCollection<SystemProcess>(StationManager.DataStorage.ProcessList);
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            StartWorkingThread();
            StartRefreshingThread();
            StationManager.StopThreads += StopWorkingThread;
            StationManager.StopThreads += StopRefreshingThread;
        }


        public override void Refresh()
        {
            Processes = new ObservableCollection<SystemProcess>(StationManager.DataStorage.ProcessList);
        }


        public ObservableCollection<SystemProcess> Processes
        {
            get { return _processes; }

            private set
            {
                _processes = value;
                OnPropertyChanged();
            }
        }

        public SystemProcess SelectedProcess
        {
            set
            {
                StationManager.CurrentProcess = value;
                OnPropertyChanged();
            }
        }


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
        

        private void StartRefreshingThread()
        {
            _refreshingThread = new Thread(RefreshingThreadProcess);
            _refreshingThread.Start();
        }

        private void StartWorkingThread()
        {
            _workingThread = new Thread(WorkingThreadProcess);
            _workingThread.Start();
        }

        private void RefreshingThreadProcess()
        {
            while (!_token.IsCancellationRequested)
            {
                Thread.Sleep(10000);
                var processes = new List<SystemProcess>(StationManager.DataStorage.ProcessList);
                LoaderManager.Instance.ShowLoader();

                foreach (SystemProcess process in processes)
                {
                    try
                    {
                        process.Refresh(Process.GetProcessById(process.Id));
                    }
                    catch (Exception)
                    {
                        process.Refresh(null);
                    }

                    if (_token.IsCancellationRequested)
                        break;
                }

                if (_token.IsCancellationRequested)
                    break;

                Processes = new ObservableCollection<SystemProcess>(processes);
                StationManager.DataStorage.ProcessList = processes;
               
                LoaderManager.Instance.HideLoader();
            }
        }

        private void WorkingThreadProcess()
        {
            while (!_token.IsCancellationRequested)
            {
                Thread.Sleep(30000);
                var processes = new List<SystemProcess>();
                LoaderManager.Instance.ShowLoader();

                foreach (Process process in Process.GetProcesses())
                {
                    SystemProcess sysProcess = StationManager.DataStorage.GetProcessById(process.Id);
                    if (sysProcess == null)
                        processes.Add(new SystemProcess(process));
                    else
                    {
                        sysProcess.Refresh(process);
                        processes.Add(sysProcess);
                    }
                    if (_token.IsCancellationRequested)
                        break;
                }

                if (_token.IsCancellationRequested)
                    break;
                Processes = new ObservableCollection<SystemProcess>(processes);
                StationManager.DataStorage.ProcessList = processes;
                if (_token.IsCancellationRequested)
                    break;

                LoaderManager.Instance.HideLoader();
            }
        }


        internal void StopWorkingThread()
        {
            _tokenSource.Cancel();
            _workingThread.Join(5000);
            _workingThread.Abort();
            _workingThread = null;
        }


        internal void StopRefreshingThread()
        {
            _tokenSource.Cancel();
            _refreshingThread.Join(2000);
            _refreshingThread.Abort();
            _refreshingThread = null;
        }

    }
}
