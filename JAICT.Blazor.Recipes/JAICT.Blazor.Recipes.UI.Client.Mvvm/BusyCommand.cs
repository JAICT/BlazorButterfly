using JAICT.Blazor.Recipes.UI.Client.Mvvm;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using System;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm
{
    /// <summary>
    /// Command that indicates that an action is ongoing in the background.
    /// </summary>
    public class BusyCommand : Command
    {
        private readonly IViewModel _viewModel;
        public BusyCommand(IViewModel viewModel, Action<object> command) : base(command)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public BusyCommand(IViewModel viewModel, Func<Task> taskFactory) : base(taskFactory)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public Func<Task> AfterExecuteSucceeded
        {
            get; set;
        }

        public Func<Task> BeforeExecute
        {
            get; set;
        }

        public Func<Task> AfterExecuteFailed
        {
            get; set;
        }

        public override async Task Execute(object parameter = null)
        {
            try
            {
                _viewModel.Error = string.Empty;
                try
                {
                    await _viewModel.SetBusy();
                    if (BeforeExecute != null)
                        await BeforeExecute();
                    await base.Execute(parameter);
                }
                finally
                {
                    await _viewModel.EndBusy();
                }
                if (AfterExecuteSucceeded != null)
                    await AfterExecuteSucceeded();
            }
            catch (InvalidOperationException e)
            {
                _viewModel.Error = e.Message;
                if (AfterExecuteFailed != null)
                    await AfterExecuteFailed();
            }
        }
    }
}
