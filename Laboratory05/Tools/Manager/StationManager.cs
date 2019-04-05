using System;
using System.Diagnostics;
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


        internal static void CloseApp()
        {
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
