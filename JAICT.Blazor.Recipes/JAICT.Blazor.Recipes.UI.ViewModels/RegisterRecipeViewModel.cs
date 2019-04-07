using JAICT.Blazor.Recipes.Application;
using JAICT.Blazor.Recipes.Entities;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public class RegisterRecipeViewModel : ViewModelBase
    {
        private readonly IViewModelRepository _viewModelRepository;
        private readonly ISessionStateManager _sessionStateManager;
        private readonly ICommandFactory _commandFactory;
        public RegisterRecipeViewModel(IViewModelRepository viewModelRepository, ISessionStateManager sessionStateManager, ICommandFactory commandFactory)
        {
            _viewModelRepository = viewModelRepository ?? throw new ArgumentNullException(nameof(viewModelRepository));
            _sessionStateManager = sessionStateManager ?? throw new ArgumentNullException(nameof(sessionStateManager));
            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));

            SeasonSelectionModelManager = new ViewModelManager<SelectionViewModel<Season>>(CreateSeasonSelectionViewModel());
            IngredientTypeSelectionManager = new ViewModelManager<SelectionViewModel<IngredientCharacteristics>>(CreateIngredientCharacteristicSelectionViewModel());

            RegisterRecipeCommand = _commandFactory.CreateCommand(this, RegisterRecipe);
        }

        public string RecipeName
        {
            get; set;
        }

        public ICommand RegisterRecipeCommand
        {
            get;set;
        }

        private async Task RegisterRecipe()
        {
            // Usually properly disable the button in case of an incorrect recipe.
            await _sessionStateManager.CreateRecipe(
                RecipeName,
                SeasonSelectionModelManager.CurrentTyped.GetSelectedValue(),
                IngredientTypeSelectionManager.CurrentTyped.GetSelectedValue());

            var sessionState = await _sessionStateManager.UpdateRecipes();
            
            // Notify any parent model about session state changes.
            await NotifyChanges(this, sessionState);

            // Notify the user about success / or failure here of course(!)
        }        

        private SelectionViewModel<Season> CreateSeasonSelectionViewModel()
        {
            return _viewModelRepository.CreateViewModel<SelectionViewModel<Season>>(this,
                (vm) => vm.FillSelectionList(new[] {
                    Tuple.Create("All Seasons", Season.All),
                    Tuple.Create("Summer", Season.Summer),
                    Tuple.Create("Winter", Season.Winter),
                    Tuple.Create("Spring", Season.Spring),
                    Tuple.Create("Autumn", Season.Autumn)}));            
        }

        private SelectionViewModel<IngredientCharacteristics> CreateIngredientCharacteristicSelectionViewModel()
        {
            return _viewModelRepository.CreateViewModel<SelectionViewModel<IngredientCharacteristics>>(this,
                (vm) => vm.FillSelectionList(new[] {
                    Tuple.Create("No specific characteristics", IngredientCharacteristics.All),
                    Tuple.Create("Fish", IngredientCharacteristics.Fish),
                    Tuple.Create("Meat", IngredientCharacteristics.Meat)}));
        }
        
        public ViewModelManager<SelectionViewModel<Season>> SeasonSelectionModelManager { get; }

        public ViewModelManager<SelectionViewModel<IngredientCharacteristics>> IngredientTypeSelectionManager { get; }
    }
}
