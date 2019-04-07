using JAICT.Blazor.Recipes.Entities;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    { 
        public Ingredient Ingredient { get; set; }

    }
}
