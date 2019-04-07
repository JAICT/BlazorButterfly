using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(IViewModel viewModel, Func<Task> commandTask);

        ICommand CreateCommand(Action<object> action);

        ICommand CreateCommand(Func<Task> commandTask);
    }
}
