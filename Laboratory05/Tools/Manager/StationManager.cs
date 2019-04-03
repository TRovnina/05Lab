using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Laboratory05.Models;
using Laboratory05.Tools.DataStorage;

namespace Laboratory05.Tools.Manager
{
    internal static class StationManager
    {
        public static event Action StopThreads;
        private static IDataStorage _dataStorage;
       
        internal static SystemProcess CurrentProcess{ get; set; }

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
            GetProcesses();
        }

        private static void GetProcesses()
        {
                foreach (Process process in Process.GetProcesses())
                {
                    DataStorage.AddProcess(new SystemProcess(process));
                }
           
        }


        //private static void RefreshProcess(SystemProcess sysProcess)
        //{
        //    //LoaderManager.Instance.ShowLoader();
        //    //await Task.Run(() =>
        //    //{
        //    //Thread.Sleep(1000);
        //    Process process = Process.GetProcessById(sysProcess.Id);
        //    if(process == null)
        //        DataStorage.DeleteProcess(sysProcess);
        //    else
        //    {
        //        sysProcess.Threads = process.Threads.Count;
        //        sysProcess.ModulesCollection = process.Modules;
        //        sysProcess.ThreadsCollection = process.Threads;
        //        sysProcess.CPU = new PerformanceCounter("Process", "% Processor Time", sysProcess.Name, true).NextValue();
        //        sysProcess.Memory = new PerformanceCounter("Process", "Working Set", sysProcess.Name, true).NextValue();
        //        sysProcess.IsActive = process.Responding;
        //    }
        //    //});

        //    //    LoaderManager.Instance.HideLoader();
        //}
        

        internal static void CloseApp()
        {
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
