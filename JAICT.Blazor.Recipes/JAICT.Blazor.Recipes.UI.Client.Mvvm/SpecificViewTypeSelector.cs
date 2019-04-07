using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm
{
    public class SpecificViewTypeSelector<T> : IViewTypeSelector where T : IView
    {
        public Type SelectViewType(string viewModelType, object selectionParameter = null)
        {
            return typeof(T);
        }
    }
}
