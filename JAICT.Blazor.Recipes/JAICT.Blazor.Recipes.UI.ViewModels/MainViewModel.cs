using JAICT.Blazor.Recipes.Application;
using JAICT.Blazor.Recipes.UI.Client.Mvvm;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ISessionStateManager _sessionStateManager;
        private readonly IViewModelRepository _viewModelRepository;

        public MainViewModel(ISessionStateManager sessionStateManager, IViewModelRepository viewModelRepository)
        {            
            Debug.WriteLine("MainViewModel created");
            AccessKey = "Mijn access key";

            _sessionStateManager = sessionStateManager ?? throw new ArgumentNullException(nameof(sessionStateManager));
            _viewModelRepository = viewModelRepository ?? throw new ArgumentNullException(nameof(viewModelRepository));
        }

        public override async Task HandleChanges(object sender, object changes)
        {
            if (changes is ISessionState sessionState)
            {
                var navigationItems = NavigationItems;
                // We'd usually do some proper login checks here!
                if (sessionState.IsLoggedIn)
                {
                    navigationItems.Add(new Navigation(() => ContentViewModelManager.Update(_viewModelRepository.CreateViewModel<RegisterRecipeViewModel>(this)), "Register Recipe"));
                    navigationItems.Add(new Navigation(() => ContentViewModelManager.Update(_viewModelRepository.CreateViewModel<RecipeSummaryViewModel>(this, m => m.InitializeComponents(sessionState.Recipes))), "Your Recipes"));
                    InformationViewModelManager.Update(_viewModelRepository.CreateViewModel<InformationViewModel>(this, (vm) => vm.OrganisationName = sessionState.Organisation.OrganisationName));
                }
                else
                {
                    InformationViewModelManager.Update(_viewModelRepository.CreateViewModel<InformationViewModel>(this, (vm) => vm.OrganisationName = string.Empty));
                }

                Debug.WriteLine("NavigationMenuModel.UpdateMenu state : " + sessionState.AccessKey);
                await Task.Run(() => NavigationMenuModelManager.Update(_viewModelRepository.CreateViewModel<NavMenuViewModel>(this, (nm) => nm.InitializeMenu(navigationItems, sessionState))));
                Debug.WriteLine("No SessionStateManager_SessionUpdated state ends");
            }
        }

        private List<INavigation> NavigationItems
        {
            get
            {
                List<INavigation> list = new List<INavigation>
                {
                    new Navigation(() => ContentViewModelManager.Update(_viewModelRepository.CreateViewModel<ProvideAccessKeyViewModel>(this)), "Login / Logout"),
                    new Navigation(() => ContentViewModelManager.Update(_viewModelRepository.CreateViewModel<RegisterOrganisationViewModel>(this)), "Register Organisation")
                };
                return list;
            }
        }

        public override void Initialize(Func<bool, Task> setBusy)
        {
            base.Initialize(setBusy);
            NavigationMenuModelManager = new ViewModelManager(
                _viewModelRepository.CreateViewModel<NavMenuViewModel>(this, (nm) => nm.InitializeMenu(NavigationItems)));
            ContentViewModelManager = new ViewModelManager(_viewModelRepository.CreateViewModel<ProvideAccessKeyViewModel>(this));
            InformationViewModelManager= new ViewModelManager(_viewModelRepository.CreateViewModel<InformationViewModel>(this));
        }

        public override async Task InitializeAsync(Func<bool, Task> setBusy)
        {
            await base.InitializeAsync(setBusy);
            var sessionState = await _sessionStateManager.GetSessionState();
            NavigationMenuModelManager.Update(_viewModelRepository.CreateViewModel<NavMenuViewModel>(this, (nm) => nm.InitializeMenu(NavigationItems, sessionState)));
        }


        public string AccessKey { get; set; }

        public IViewModelManager NavigationMenuModelManager
        {
            get; private set;            
        }

        public IViewModelManager ContentViewModelManager
        {
            get; private set;
        }       
        
        public IViewModelManager InformationViewModelManager
        {
            get; private set;
        }
    }
}
