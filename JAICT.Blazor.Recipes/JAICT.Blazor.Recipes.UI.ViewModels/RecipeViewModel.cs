using JAICT.Blazor.Recipes.Application;
using JAICT.Blazor.Recipes.Entities;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public class RecipeViewModel : ViewModelBase
    {
        private readonly ISessionStateManager _sessionStateManager;

        private readonly IViewModelRepository _viewModelRepository;

        private readonly ICommandFactory _commandFactory;

        public RecipeViewModel(ISessionStateManager sessionStateManager, IViewModelRepository viewModelRepository, ICommandFactory commandFactory)
        {
            _sessionStateManager = sessionStateManager ?? throw new ArgumentNullException(nameof(sessionStateManager));
            _viewModelRepository = viewModelRepository ?? throw new ArgumentNullException(nameof(viewModelRepository));
            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));

            RetrieveIngredientsCommand = _commandFactory.CreateCommand(this, RetrieveIngredients);
            RemoveIngredientsCommand = _commandFactory.CreateCommand(this, RemoveIngredients);
            AddIngredientCommand = _commandFactory.CreateCommand(this, AddIngredient);
        }

        public RecipeRegistration RecipeRegistration { get; set;}

        public ICommand RetrieveIngredientsCommand { get; }

        public ICommand RemoveIngredientsCommand { get; }

        public ICommand AddIngredientCommand { get; }

        private async Task RetrieveIngredients()
        {
            Debug.WriteLine("RetrieveIngredients");
            var ingredients = await _sessionStateManager.GetIngredients(RecipeRegistration);

            var ingredientViewModel = new List<IViewModel>();
            foreach (var ingredient in ingredients)
            {
                Debug.WriteLine("Creating ingredient viewmodel for " + ingredient.IngredientName);
                var cvm = _viewModelRepository.CreateViewModel<IngredientViewModel>(this, (cv) => cv.Ingredient = ingredient);
                ingredientViewModel.Add(cvm);
            }

            IngredientViewModelManager.Update(ingredientViewModel);
        }

        private async Task RemoveIngredients()
        {
            await Task.Delay(10);        
            IngredientViewModelManager.Update(Enumerable.Empty<IViewModel>());
        }

        private async Task AddIngredient()
        {
            await Task.Delay(1);
            IngredientViewModelManager.Update(new List<AddIngredientViewModel> { _viewModelRepository.CreateViewModel<AddIngredientViewModel>(this) });
        }



        public IViewModelCollectionManager IngredientViewModelManager { get; } = new ViewModelCollectionManager();
    }
}
