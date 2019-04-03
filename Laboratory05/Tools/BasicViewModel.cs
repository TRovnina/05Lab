using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laboratory05.Tools
{
    internal abstract class BasicViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public abstract void Refresh();

    }
}
