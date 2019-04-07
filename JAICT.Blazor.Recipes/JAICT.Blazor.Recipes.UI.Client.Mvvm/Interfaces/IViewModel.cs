using System;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces
{
    public interface IViewModel
    {
        void Initialize(Func<bool,Task> setBusy);
        Task InitializeAsync(Func<bool,Task> setBusy);

        Task NotifyChanges(object sender, object changes);

        IViewModel ParentModel { get; }

        bool IsViewModelRoot { get; }

        Task SetBusy();

        Task EndBusy();

        string Error { get; set; }
    }
}
