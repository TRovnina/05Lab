using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Laboratory05.Models;
using Laboratory05.Tools.Manager;

namespace Laboratory05.ViewModel
{
    internal class ProcessListViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<SystemProcess> _processes;
        private Thread _workingThread;
        private Thread _refreshingThread;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;

        public ObservableCollection<SystemProcess> Processes
        {
            get { return _processes;}

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

        internal ProcessListViewModel()
        {
            _processes = new ObservableCollection<SystemProcess>(StationManager.DataStorage.ProcessList);
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            StartWorkingThread();
            StartRefreshingThread();
            StationManager.StopThreads += StopWorkingThread;
            StationManager.StopThreads += StopRefreshingThread;
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
            while (true)
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
                   
                }

                Processes = new ObservableCollection<SystemProcess>(processes);
                StationManager.DataStorage.ProcessList = processes;
                if (_token.IsCancellationRequested)
                    break;

                LoaderManager.Instance.HideLoader();
            }
        }

        private void WorkingThreadProcess()
         {
             while (true)
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
                }

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
            _workingThread.Join(2000);
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


        public void Refresh()
        {
            Processes = new ObservableCollection<SystemProcess>(StationManager.DataStorage.ProcessList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
