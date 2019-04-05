using System;
using System.Diagnostics;
using System.Management;

namespace Laboratory05.Models
{
    public class SystemProcess
    {
        #region Fields
        private readonly int _id;
        private readonly string _name;
        private bool _isActive;
        private double _cpu;
        private double _memory;
        private double _memoryPercent;
        private int _threads;
        private readonly string _user;
        private readonly string _path;
        private readonly DateTime _start;
        private ProcessModuleCollection _modulesCollection;
        private ProcessThreadCollection _threadsCollection;

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
           
        }

        public string Name
        {
            get { return _name; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            private set
            {
                _isActive = value;
            }
        }


        public double CPU
        {
            get { return _cpu; }
            private set
            {
                _cpu = value;

            }
        }


        public double Memory
        {
            get { return _memory; }
            private set
            {
                _memory = value;

            }
        }

        public double MemoryPercent
        {
            get { return _memoryPercent; }
            private set
            {
                _memoryPercent = value;

            }
        }


        public int Threads
        {
            get { return _threads; }
            private set
            {
                _threads = value;
            }
        }


        public string User
        {
            get { return _user; }
        }


        public string Path
        {
            get { return _path; }
        }

        public DateTime Start
        {
            get { return _start; }
        }


        public ProcessModuleCollection ModulesCollection
        {
            get { return _modulesCollection; }
            private set
            {
                _modulesCollection = value;
            }
        }

        public ProcessThreadCollection ThreadsCollection
        {
            get { return _threadsCollection; }
            private set
            {
                _threadsCollection = value;
            }
        }

        #endregion


        #region Constructors
        
       public SystemProcess(Process process)
       {
            _id = process.Id;
            _name = process.ProcessName;
            _user = GetUser();
            
            try
            {
                _path = process.MainModule.FileName;
            }
            catch (Exception)
            {
                _path = "";
            }

            try
            {
                _start = process.StartTime;
            }
            catch (Exception)
            {
            }

            Refresh(process);
       }


       public void Refresh(Process process)
       {
           try
           {
               IsActive = process.Responding;
           }
            catch (Exception)
           {
               IsActive = false;
           }


            try
           {
               CPU = (double)new PerformanceCounter("Process", "% Processor Time", Name, true).NextValue() / Environment.ProcessorCount;
           }
           catch (Exception)
           {
               CPU = 0;
           }

           try
           {
               Memory = new PerformanceCounter("Process", "Working Set", Name, true).NextValue() * 0.0001;
               double computerMemory = new PerformanceCounter("Memory", "Available MBytes").NextValue();
               MemoryPercent = Memory / computerMemory;
           }
           catch (Exception)
           {
               Memory = 0;
           }

           try
           {
               ThreadsCollection = process.Threads;
               Threads = process.Threads.Count;
           }
           catch (Exception)
           {
           }

           try
           {
               ModulesCollection = process.Modules;
           }
           catch (Exception)
           {
           }
       }

        #endregion


        public override string ToString()
        {
            return $"{Id} {Name}";
        }


        private string GetUser()
        {
            string query = "Select * From Win32_Process Where ProcessID = " + Id;
            ManagementObjectCollection processList = new ManagementObjectSearcher(query).Get();

            foreach (var o in processList)
            {
                var obj = (ManagementObject) o;
                object[] argList = { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    return "" + argList[0];
                }
            }
            return "";
        }

    }

}
