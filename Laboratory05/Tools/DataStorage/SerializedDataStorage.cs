using System.Collections.Generic;
using System.Linq;
using Laboratory05.Models;

namespace Laboratory05.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private List<SystemProcess> _processes;

        internal SerializedDataStorage()
        {
            ProcessList = new List<SystemProcess>();
        }

        public bool ProcessExists(int id)
        {
            return _processes.Exists(p => p.Id == id);
        }

        public SystemProcess GetProcessById(int id)
        {
            return _processes.FirstOrDefault(p => p.Id == id);
        }
        
        public void DeleteProcess(SystemProcess process)
        {
            ProcessList.Remove(process);
        }

        public void SortList(string parameter)
        {
            if (parameter.Equals("name"))
                ProcessList = new List<SystemProcess>(_processes.OrderBy(p => p.Name).ToList());
            else if (parameter.Equals("id"))
                ProcessList = new List<SystemProcess>(_processes.OrderBy(p => p.Id).ToList());
            else if (parameter.Equals("active"))
                ProcessList = new List<SystemProcess>(_processes.OrderBy(p => p.IsActive).ToList());
            else if (parameter.Equals("cpu"))
                ProcessList = new List<SystemProcess>(_processes.OrderBy(p => p.CPU).ToList());
            else if (parameter.Equals("memory"))
                ProcessList = new List<SystemProcess>(_processes.OrderBy(p => p.Memory).ToList());
            else if (parameter.Equals("threads"))
                ProcessList = new List<SystemProcess>(_processes.OrderBy(p => p.Threads).ToList());
            else if (parameter.Equals("user"))
                ProcessList = new List<SystemProcess>(_processes.OrderBy(p => p.User).ToList());
            else if (parameter.Equals("path"))
                ProcessList = new List<SystemProcess>(_processes.OrderBy(p => p.Path).ToList());
            else if (parameter.Equals("start"))
                ProcessList = new List<SystemProcess>(_processes.OrderBy(p => p.Start).ToList());
        }

        public void AddProcess(SystemProcess process)
        {
            ProcessList.Add(process);
        }

        public List<SystemProcess> ProcessList
        {
            get { return _processes; }
            set
            {
                _processes = value;
            }
        }
    }
}
