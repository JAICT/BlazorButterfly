using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces
{
    public interface ICommand
    {
        Task Execute(object parameter = null);

        object CommandParameter { get; set; }

        bool CanExecute(object parameter);
    }
}
