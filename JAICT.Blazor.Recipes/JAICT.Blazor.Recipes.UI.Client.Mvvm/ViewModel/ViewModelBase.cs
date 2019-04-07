using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.View;
using System;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel
{
    public class ViewModelChangedEventArgs : EventArgs
    {
        public IViewModel NewViewModel { get; set; }
    }

    public abstract class ViewModelBase : IViewModel
    {
        public delegate void ViewModelReplacedEventHandler(object sender, ViewModelChangedEventArgs args);

        public IViewModel ParentModel { get; private set; }

        public bool IsViewModelRoot { get; private set; }

        private Func<bool,Task> _setBusy;
        public ViewModelBase(IViewModel viewModel)
        {
            ParentModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            IsViewModelRoot = false;
        }

        public ViewModelBase()
        {
            IsViewModelRoot = true;
        }

        public async Task NotifyChanges(object sender, object changes)
        {
            await HandleChanges(sender, changes);
            if (ParentModel!=null)
            {
                await ParentModel.NotifyChanges(sender, changes);
            }
        }

        public virtual async Task HandleChanges(object sender, object changes)
        {
            await Task.Run(() => { });
        }

        public void SetParent(IViewModel parentModel)
        {
            IsViewModelRoot = false;
            ParentModel = parentModel ?? throw new ArgumentNullException(nameof(parentModel));
        }

        /// <summary>
        /// Either override Initialize or InitializeAsync or none. Do not override both
        /// </summary>
        public virtual void Initialize(Func<bool,Task> setBusy)
        {
            _setBusy = setBusy ?? throw new ArgumentNullException(nameof(setBusy));
        }

        /// <summary>
        /// Either override Initialize or InitializeAsync or none. Do not override both
        /// </summary>
        public virtual async Task InitializeAsync(Func<bool, Task> setBusy)
        {
            _setBusy = setBusy ?? throw new ArgumentNullException(nameof(setBusy));
            // Do nothing here
            await Task.Run(() => { });
        }

        public async Task SetBusy()
        {
            await _setBusy(true);
        }

        public async Task EndBusy()
        {
            await _setBusy(false);
        }

        public string Error { get; set; }
    }
}
