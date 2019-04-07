using JAICT.Blazor.Recipes.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JAICT.Blazor.Recipes.Application
{
    public class SessionState : ISessionState
    {
        public const string SessionKey = "BlazorRecipesOrganisationSessionKey";

        [JsonProperty]
        public string AccessKey { get; set; }

        [JsonProperty]
        public OrganisationRegistration Organisation { get; set; }

        [JsonProperty]
        public IEnumerable<RecipeRegistration> Recipes { get; set; } = new List<RecipeRegistration>();

        public void ClearState()
        {
            AccessKey = string.Empty;
            Organisation = null;
            Recipes = new List<RecipeRegistration>();
        }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrEmpty(AccessKey) && Organisation != null;
            }
        }
    }
}
