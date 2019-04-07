using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm
{
    public class Navigation : INavigation
    {
        private readonly Action _navigateAction;
        public Navigation(Action navigationTask, string navigationName)
        {
            NavigationName = navigationName;
            _navigateAction = navigationTask ?? throw new ArgumentNullException(nameof(navigationTask));
        }

        public string NavigationName { get; }

        public void Navigate()
        {
            Debug.WriteLine("Navigate called!");
            _navigateAction();
        }
    }
}
