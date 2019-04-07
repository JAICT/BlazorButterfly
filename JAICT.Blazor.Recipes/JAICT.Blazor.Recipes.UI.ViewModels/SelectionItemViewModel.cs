using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public abstract class SelectionItemViewModel : ViewModelBase
    {
        public string Description { get; set; }

        public int Index { get; set; }
    }

    public class SelectionItemViewModel<T> : SelectionItemViewModel where T : struct
    {
        public T Value { get; set; }
    }
}
