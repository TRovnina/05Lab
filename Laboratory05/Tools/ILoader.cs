using System.ComponentModel;
using System.Windows;

namespace Laboratory05.Tools
{
    internal interface ILoader : INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }
}
