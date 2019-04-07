using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm
{
    public class Command : ICommand
    {
        private readonly Action<object> _command;

        private readonly Func<Task> _task;

        public Command(Action<object> command)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public Command(Func<Task> task)
        {
            _task = task ?? throw new ArgumentNullException(nameof(task));
        }

        public object CommandParameter { get; set; }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual async Task Execute(object parameter = null)
        {            
            if (_command != null)
            {
                await new Task(() => _command(parameter));
                return;
            }

            if (_task != null)
            {
                await _task();
                return;
            }
        }
    }
}
