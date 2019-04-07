using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel
{
    public class ViewModelRepository : IViewModelRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IViewRepository _viewRepository;

        public ViewModelRepository(IServiceProvider serviceProvider, IViewRepository viewRepository)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException();
            _viewRepository = viewRepository ?? throw new ArgumentException(nameof(viewRepository));
        }

        private static Dictionary<string, Type> _viewModelTypes;

        private static readonly object _lockObject = new object();

        private static void LoadViewModelTypes()
        {
            if (_viewModelTypes != null)
                return;

            lock (_lockObject)
            {
                if (_viewModelTypes != null)
                    return;

                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                _viewModelTypes = assemblies.SelectMany(a => a.GetTypes())
                    .Where(t => !t.IsAbstract)
                    .Where(t => t.GetInterfaces()
                    .Any(x => x == typeof(IViewModel)))
                    .ToDictionary(s => s.Name);

                foreach (var item in _viewModelTypes)
                {
                    System.Diagnostics.Debug.WriteLine("ViewModel Type " + item.Key);
                }
            }
        }

        public static IEnumerable<Type> GetViewModelTypes(IEnumerable<string> additionalAssemblies = null)
        {
            if (additionalAssemblies != null)
            {
                foreach (var assemblyName in additionalAssemblies)
                {
                    Assembly.Load(assemblyName);
                }
            }

            LoadViewModelTypes();
            return _viewModelTypes.Values;
        }

        public Type FindViewType(string viewModelName)
        {
            LoadViewModelTypes();
            if (viewModelName.ToUpperInvariant().EndsWith("VIEWMODEL"))
            {
                System.Diagnostics.Debug.WriteLine($"New viewModel {viewModelName}");
                if (_viewModelTypes.Any(kvp => kvp.Key.ToUpperInvariant() == viewModelName.ToUpperInvariant()))
                {
                    var viewName = viewModelName.Substring(0, viewModelName.Length - "MODEL".Length);
                    System.Diagnostics.Debug.WriteLine("Searching for view name  " + viewName);

                    return _viewRepository.GetViewType(viewName);
                }
                else
                    throw new ArgumentException($"{viewModelName} could not be found....");
            }
            else
                throw new ArgumentException($"{viewModelName} could not be found....");
        }

        public Type FindViewModelType(string viewModelName)
        {
            if (_viewModelTypes.ContainsKey(viewModelName))
            {
                return _viewModelTypes[viewModelName];
            }
            return null;
        }
        
        public IViewModel CreateViewModel(string viewModelName, IViewModel parentModel)
        {
            var viewModel = CreateViewModel(viewModelName);

            if (viewModel is ViewModelBase viewModelBase)
            {
                viewModelBase.SetParent(parentModel);
            }

            return viewModel;
        }

        public IViewModel CreateViewModel(string viewModelName)
        {
            LoadViewModelTypes();
            if (!_viewModelTypes.ContainsKey(viewModelName))
                throw new ArgumentException("The viewmodel name is invalid");

            return _serviceProvider.GetService(_viewModelTypes[viewModelName])as IViewModel;
        }        

        public T CreateViewModel<T>(IViewModel parentModel, Action<T> postConstructionAction = null) where T : ViewModelBase
        {
            var t = _serviceProvider.GetService(typeof(T)) as T;
            t.SetParent(parentModel);
            postConstructionAction?.Invoke(t);
            return t;
        }        
    }
}
