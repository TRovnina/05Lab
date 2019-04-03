using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Laboratory05.Models;
using Laboratory05.Tools.Manager;

namespace Laboratory05.ViewModel
{
    class ProcessListViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<SystemProcess> _processes;
        private Thread _workingThread;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;

        public ObservableCollection<SystemProcess> Processes
        {
            get { return _processes;}

            private set { _processes = value; }
        }

        public SystemProcess SelectedProcess
        {
            get { return StationManager.CurrentProcess; }

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
            StationManager.StopThreads += StopWorkingThread;
        }

        private void StartWorkingThread()
        {
            _workingThread = new Thread(WorkingThreadProcess);
            _workingThread.Start();
        }

         private void WorkingThreadProcess()
         {
             while (!_token.IsCancellationRequested)
             {
                 Thread.Sleep(20000);
                 var processes = _processes.ToList();
                 LoaderManager.Instance.ShowLoader();
                 foreach (Process process in Process.GetProcesses())
                 {
                     processes.Add(new SystemProcess(process));
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

            public event PropertyChangedEventHandler PropertyChanged;
        
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        
    }
}
