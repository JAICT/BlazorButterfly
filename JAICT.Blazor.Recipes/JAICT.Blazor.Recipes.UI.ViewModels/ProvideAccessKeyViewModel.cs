using JAICT.Blazor.Recipes.Application;
using JAICT.Blazor.Recipes.UI.Client.Mvvm;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public class ProvideAccessKeyViewModel : ViewModelBase
    {
        private ISessionStateManager _sessionStateManager;

        // Prefilled login key
        private static string PreconfiguredAccessKey = "ef269f3d-8f1d-4693-bbc1-06dce4956d8ef03c21aa-4b58-4636-b1e5-e1533c15a5a4";

        public ProvideAccessKeyViewModel(ISessionStateManager sessionStateManager)
        {
            _sessionStateManager = sessionStateManager ?? throw new ArgumentNullException(nameof(sessionStateManager));
            LoginCommand = new BusyCommand(this, CheckAccessKey);
            LogoutCommand = new BusyCommand(this, RemoveAccessKey);

            // Some preconfigured access key for testing
            AccessKey = PreconfiguredAccessKey;
        }

        public ICommand LoginCommand { get; set; }

        public ICommand LogoutCommand { get; set; }

        public string AccessKey { get; set; }

        private async Task CheckAccessKey()
        {
            Debug.WriteLine("Register");
            if (string.IsNullOrWhiteSpace(AccessKey))
                return;

            var sessionState = await _sessionStateManager.Login(AccessKey);

            await NotifyChanges(this, sessionState);            

            Debug.WriteLine("Register completed");
        }

        private async Task RemoveAccessKey()
        {
            // Temporarily needed to remove the one time fill of the access key.
            PreconfiguredAccessKey = null;
            AccessKey = null;

            var sessionState = await _sessionStateManager.GetSessionState();
            if (sessionState != null)
                sessionState.ClearState();

            await _sessionStateManager.UpdateSessionState(sessionState);

            // Notify any parent model about session state changes.
            await NotifyChanges(this, sessionState);
        }
    }
}
