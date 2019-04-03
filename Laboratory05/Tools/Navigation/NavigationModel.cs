using System.Collections.Generic;

namespace Laboratory05.Tools.Navigation
{
    internal abstract class NavigationModel : INavigation
    {
        private readonly IContent _content;
        private readonly Dictionary<ViewType, INavigatable> _viewsDictionary;

        protected NavigationModel(IContent content)
        {
            _content = content;
            _viewsDictionary = new Dictionary<ViewType, INavigatable>();
        }

        protected IContent Content
        {
            get { return _content; }
        }

        protected Dictionary<ViewType, INavigatable> ViewsDictionary
        {
            get { return _viewsDictionary; }
        }

        public void Navigate(ViewType viewType)
        {
            if (!ViewsDictionary.ContainsKey(viewType))
                InitializeView(viewType);
            Content.ContentControl.Content = ViewsDictionary[viewType].Refresh();
        }

        protected abstract void InitializeView(ViewType viewType);

    }

}
