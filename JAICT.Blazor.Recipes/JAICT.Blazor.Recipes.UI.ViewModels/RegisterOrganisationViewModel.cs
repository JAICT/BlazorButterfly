using JAICT.Blazor.Recipes.Application;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{
    public class RegisterOrganisationViewModel : ViewModelBase
    {
        private ISessionStateManager _sessionStateManager;

        private readonly ICommandFactory _commandFactory;

        public RegisterOrganisationViewModel(ISessionStateManager sessionStateManager, ICommandFactory commandFactory)
        {
            _sessionStateManager = sessionStateManager ?? throw new ArgumentNullException(nameof(_sessionStateManager));
            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));

            RegisterCommand = _commandFactory.CreateCommand(this, RegisterOrganisation);
        }

        private string _organisationName;
        public string OrganisationName
        {
            get
            {
                return _organisationName;
            }
            set
            {
                _organisationName = value;
                Debug.WriteLine("OrganisationName = " + value);
            }
        }

        public string AccessKey { get; private set; }

        public ICommand RegisterCommand
        {
            get; private set;
        }

        public async Task RegisterOrganisation()
        {
            Debug.WriteLine("Calling RegisterOrganisation");
            AccessKey = string.Empty;
            Error = string.Empty;

            if (string.IsNullOrEmpty(OrganisationName))
                throw new ArgumentException(nameof(OrganisationName) + " can not be empty");

            var sessionState = await _sessionStateManager.RegisterOrganisation(OrganisationName);
            AccessKey = sessionState?.AccessKey;
        }
    }
}
