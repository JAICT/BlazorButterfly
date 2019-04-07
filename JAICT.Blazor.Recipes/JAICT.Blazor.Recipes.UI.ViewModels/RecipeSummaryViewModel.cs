using JAICT.Blazor.Recipes.Entities;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Collections.Generic;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public class RecipeSummaryViewModel : ViewModelBase
    {
        private IViewModelRepository _viewModelRepository;
        public RecipeSummaryViewModel(IViewModelRepository repository)
        {
            _viewModelRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void InitializeComponents(IEnumerable<RecipeRegistration> recipes)
        {
            if (recipes == null)
                throw new ArgumentNullException(nameof(recipes));

            List<IViewModel> recipeViewModels = new List<IViewModel>();
            foreach(var item in recipes)
            {
                var cvm = _viewModelRepository.CreateViewModel<RecipeViewModel>(this, (cv) => cv.RecipeRegistration = item);
                recipeViewModels.Add(cvm);
            }

            RecipeViewModelManager.Update(recipeViewModels);
        }

        public IViewModelCollectionManager RecipeViewModelManager { get; } = new ViewModelCollectionManager();
    }
}
