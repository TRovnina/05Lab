using System.Collections.Generic;
using Laboratory05.Models;

namespace Laboratory05.Tools.DataStorage
{
    internal interface IDataStorage
    {
        bool ProcessExists(int id);
        SystemProcess GetProcessById(int id);
        List<SystemProcess> ProcessList { get; set; }
        void DeleteProcess(SystemProcess process);
        void AddProcess(SystemProcess process);
        void SortList(string parameter);
    }
}
