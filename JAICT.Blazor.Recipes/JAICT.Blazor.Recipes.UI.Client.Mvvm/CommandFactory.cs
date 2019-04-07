using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm
{
    public class CommandFactory : ICommandFactory
    {
        /// <summary>
        /// Create a command that indicates a busy state.
        /// </summary>
        /// <param name="viewModel">The current viewmodel the command is created from</param>
        /// <param name="commandTask">The task to execute</param>
        /// <returns>The command interface</returns>
        public ICommand CreateCommand(IViewModel viewModel, Func<Task> commandTask)
        {
            return new BusyCommand(viewModel, commandTask);
        }

        public ICommand CreateCommand(Action<object> action)
        {
            return new Command(action);
        }

        public ICommand CreateCommand(Func<Task> commandTask)
        {
            return new Command(commandTask);
        }
    }
}
