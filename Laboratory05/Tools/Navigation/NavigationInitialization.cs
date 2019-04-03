using System;
using Laboratory05.View;

namespace Laboratory05.Tools.Navigation
{
    internal class NavigationInitialization : NavigationModel
    {
        public NavigationInitialization(IContent content) : base(content)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Modules:
                    ViewsDictionary.Add(viewType, new ModulesView());
                    break;
                case ViewType.Threads:
                    ViewsDictionary.Add(viewType, new ThreadsView());
                    break;
                case ViewType.MainPage:
                    ViewsDictionary.Add(viewType, new MainPageView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
