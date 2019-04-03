using System;
using System.Diagnostics;

namespace Laboratory05.Models
{
    internal class SystemProcess
    {
        #region Fields
        private readonly int _id;
        private readonly string _name;
        private bool _isActive;
        private double _cpu;
        private double _memory;
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
            set
            {
                _isActive = value;
            }
        }


        public double CPU
        {
            get { return _cpu; }
            set
            {
                _cpu = value;

            }
        }


        public double Memory
        {
            get { return _memory; }
            set
            {
                _memory = value;

            }
        }


        public int Threads
        {
            get { return _threads; }
            set
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
            set
            {
                _modulesCollection = value;
            }
        }

        public ProcessThreadCollection ThreadsCollection
        {
            get { return _threadsCollection; }
            set
            {
                _threadsCollection = value;
            }
        }

        #endregion


        #region Constructors

        //public SystemProcess(int id, string name, double cpu, double memory,  int threads, string user, string path, DateTime start)
        //{
        //    _id = id;
        //    _name = name;
        //    _isActive = true;
        //    _cpu = cpu;
        //    _memory = memory;
        //    _threads = threads;
        //    _user = user;
        //    _path = path;
        //    _start = start;
        //}

        public SystemProcess(Process process)
        {
            _id = process.Id;
            _name = process.ProcessName;
            _isActive = process.Responding;
            _cpu = new PerformanceCounter("Process", "% Processor Time", Name, true).NextValue();
            _memory = new PerformanceCounter("Process", "Working Set", Name, true).NextValue();
            _user = process.StartInfo.Environment["USERNAME"];
            //_path = process.StartInfo.FileName;
            //_user = process.MachineName;
            //_path = Path.GetDirectoryName(process);

            try
            {
                _threadsCollection = process.Threads;
                _threads = process.Threads.Count;
            }
            catch (Exception)
            {
            }

            try{
                _modulesCollection = process.Modules;
            }
            catch (Exception)
            {
            }

            try
            {
                _path = process.MainModule.FileName;
            }
            catch (Exception)
            {
            }

            try
            {
                _start = process.StartTime;
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

    }

}
