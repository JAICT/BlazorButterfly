using JAICT.Blazor.Recipes.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.Application
{
    public interface ISessionStateManager
    {        
        Task UpdateSessionState(SessionState sessionState);

        Task<SessionState> GetSessionState();

        Task<SessionState> RegisterOrganisation(string organisationName);

        Task<SessionState> Login(string accessKey);

        Task CreateRecipe(string recipeName, Season environment, IngredientCharacteristics encryptionMode);

        Task<SessionState> UpdateRecipes();

        Task<IEnumerable<Ingredient>> GetIngredients(RecipeRegistration recipeRegistration);
    }
}
