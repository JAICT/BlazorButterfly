using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces
{
    public interface IViewModelManager<T> where T : class
    {
        T Current
        {
            get;
        }

        void Update(T newModel);

        event EventHandler Updated;
    }

    public interface IViewModelManager : IViewModelManager<IViewModel> { }

    public interface IViewModelCollectionManager : IViewModelManager<IEnumerable<IViewModel>> { }
}
