namespace Laboratory05.Tools.Navigation
{
    internal enum ViewType
    {
        MainPage,
        Modules,
        Threads
    }

    interface INavigation
    {
        void Navigate(ViewType viewType);
    }
}
