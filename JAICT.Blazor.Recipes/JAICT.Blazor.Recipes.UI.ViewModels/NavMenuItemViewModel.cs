using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public class NavMenuItemViewModel : ViewModelBase
    {
        public INavigation NavigationItem { get; private set; }

        public void Initialize(INavigation navigationItem)
        {
            NavigationItem = navigationItem ?? throw new ArgumentNullException(nameof(navigationItem));
            Title = navigationItem.NavigationName;
            TargetLink = navigationItem.NavigationName;
        }

        public void Initialize(string title, bool isHomeLink = false)
        {
            Title = title;
            IsHomeLink = isHomeLink;
            if (isHomeLink)
                TargetLink = string.Empty;
            else
                TargetLink = title;
        }

        public string Title { get; set; }

        public string TargetLink { get; set; }

        public bool IsHomeLink { get; set; }
    }
}
