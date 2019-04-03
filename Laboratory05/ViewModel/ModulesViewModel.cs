
using System.Diagnostics;
using System.Windows.Input;
using Laboratory05.Tools;
using Laboratory05.Tools.Manager;
using Laboratory05.Tools.Navigation;

namespace Laboratory05.ViewModel
{
    internal class ModulesViewModel : BasicViewModel
    {
        private ICommand _returnCommand;

        public ModulesViewModel()
        {
            GetModules = StationManager.CurrentProcess.ModulesCollection;
        }

        public ProcessModuleCollection GetModules { get; }

        public string SelectedProcess
        {
            get { return StationManager.CurrentProcess.ToString(); }
        }
            
        public ICommand ReturnCommand
        {
            get
            {
                return _returnCommand ?? (_returnCommand = new RelayCommand<object>(Return));
            }
        }

        private void Return(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.MainPage);
        }

        public override void Refresh()
        {
        }
    }
}
