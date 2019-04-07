using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces
{
    public interface IViewTypeSelector
    {
        Type SelectViewType(string viewModelType, object selectionParameter = null);
    }
}
