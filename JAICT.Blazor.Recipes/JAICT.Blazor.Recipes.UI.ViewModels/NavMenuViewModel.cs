using JAICT.Blazor.Recipes.Application;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public class NavMenuViewModel : ViewModelBase
    {
        private IViewModelRepository _viewModelRepository;

        public NavMenuViewModel(IViewModelRepository viewModelRepository)
        {
            _viewModelRepository = viewModelRepository ?? throw new ArgumentNullException(nameof(viewModelRepository)); ;
        }

        public IViewModelCollectionManager MenuItemCollectionManager { get; } = new ViewModelCollectionManager();

        public void InitializeMenu(IEnumerable<INavigation> navigationItems, ISessionState sessionState = null)
        {
            List<IViewModel> viewModels = new List<IViewModel>();

            Debug.WriteLine("NavMenuViewModel.InitializeMenu");
            viewModels.Add(_viewModelRepository.CreateViewModel<NavMenuItemViewModel>(this, (ni) => ni.Initialize("Home", true)));
            if (navigationItems != null)
            {
                foreach (var navigationItem in navigationItems)
                {
                    viewModels.Add(_viewModelRepository.CreateViewModel<NavMenuItemViewModel>(this, (ni) => ni.Initialize(navigationItem)));
                }
            }

            MenuItemCollectionManager.Update(viewModels);
        }
    }
}
