using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.View
{
    public class ViewRepository : IViewRepository
    {
        private static Dictionary<string, Type> _viewTypes;

        private static object _lockObject = new object();

        private static void LoadViews()
        {
            if (_viewTypes != null)
                return;

            lock (_lockObject)
            {
                if (_viewTypes != null)
                    return;

                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                _viewTypes = assemblies.SelectMany(a => a.GetTypes())
                    .Where(t => t.GetInterfaces()
                    .Any(x => x == typeof(IView)))
                    .ToDictionary(s => s.Name);

                foreach (var item in _viewTypes)
                {
                    System.Diagnostics.Debug.WriteLine("View Type " + item.Key);
                }
            }
        }

        public Type GetViewType(string viewName)
        {
            LoadViews();
            if (!_viewTypes.ContainsKey(viewName))
                throw new InvalidOperationException($"{viewName} was not found in the list of view types");

            return _viewTypes[viewName];

        }
    }
}
