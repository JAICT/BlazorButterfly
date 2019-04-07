using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces
{
    public interface IViewModelRepository
    {
        Type FindViewType(string viewModelName);        

        T CreateViewModel<T>(IViewModel parentModel, Action<T> postConstructionAction = null) where T : ViewModelBase;

        IViewModel CreateViewModel(string viewModelName);

        Type FindViewModelType(string viewModelName);
    }
}
