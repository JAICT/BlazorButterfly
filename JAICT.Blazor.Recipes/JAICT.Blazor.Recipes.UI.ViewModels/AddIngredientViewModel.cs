using JAICT.Blazor.Recipes.Entities;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public class AddIngredientViewModel : ViewModelBase
    {
        private IViewModelRepository _viewModelRepository;
        public AddIngredientViewModel(IViewModelRepository viewmodelRepository)
        {
            _viewModelRepository = viewmodelRepository ?? throw new ArgumentNullException(nameof(viewmodelRepository));
            IngredientTypeSelectionManager = new ViewModelManager<SelectionViewModel<IngredientType>>(CreateIngredientTypeSelectionViewModel());
        }

        

        private SelectionViewModel<IngredientType> CreateIngredientTypeSelectionViewModel()
        {
            return _viewModelRepository.CreateViewModel<SelectionViewModel<IngredientType>>(this,
                (vm) => vm.FillSelectionList(new[] {
                    Tuple.Create("Unknown", IngredientType.Unknown),
                    Tuple.Create("Fresh", IngredientType.Fresh),
                    Tuple.Create("Stock", IngredientType.Stock)}));
        }

        public ViewModelManager<SelectionViewModel<IngredientType>> IngredientTypeSelectionManager { get; }
    }
}
