using JAICT.Blazor.Recipes.UI.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces
{
    public interface INavigation
    {
        void Navigate();

        string NavigationName { get; }
    }
}
