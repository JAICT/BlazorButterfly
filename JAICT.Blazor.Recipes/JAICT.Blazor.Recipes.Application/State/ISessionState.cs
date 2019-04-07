using JAICT.Blazor.Recipes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.Application
{
    public interface ISessionState
    {
        string AccessKey { get; set; }

        OrganisationRegistration Organisation { get; set; }

        IEnumerable<RecipeRegistration> Recipes { get; set; }

        void ClearState();
        
        bool IsLoggedIn { get; }
    }
}
