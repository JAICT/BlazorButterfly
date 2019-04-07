using JAICT.Blazor.Recipes.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.Application
{
    public class SessionStateManager : ISessionStateManager
    {        
        private readonly IStateStorage _stateStorage;

        // In memory database
        private Dictionary<string, List<RecipeRegistration>> _registeredRecipes = new Dictionary<string, List<RecipeRegistration>>();
        private Dictionary<string, OrganisationRegistration> _registeredOrganisations = new Dictionary<string, OrganisationRegistration>();
        private readonly List<Ingredient> _bafIngredients = new List<Ingredient>
        {
            new Ingredient { IngredientName = "Potatoes", Amount = 4, AmountType = "", Description = "Potatoes for making the fries", IngredientType = IngredientType.Fresh },
            new Ingredient { IngredientName = "Minced meat", Amount = 500, AmountType = "grams", Description = "Minced beef for making the burger", IngredientType = IngredientType.Fresh },
            new Ingredient { IngredientName = "Lettuce", Amount = 100, AmountType = "gram", Description = "Mixed lettuce", IngredientType = IngredientType.Fresh },
            new Ingredient { IngredientName = "Onion", Amount = 1, AmountType = "", Description = "To add to the burger", IngredientType = IngredientType.Fresh }
        };

        public SessionStateManager(IStateStorage stateStorage)
        {
            _stateStorage = stateStorage ?? throw new ArgumentNullException(nameof(stateStorage));            

            _registeredOrganisations.Add("ef269f3d-8f1d-4693-bbc1-06dce4956d8ef03c21aa-4b58-4636-b1e5-e1533c15a5a4", new OrganisationRegistration { OrganisationName = "Blazor MealService" });
            _registeredRecipes.Add("ef269f3d-8f1d-4693-bbc1-06dce4956d8ef03c21aa-4b58-4636-b1e5-e1533c15a5a4", new List<RecipeRegistration> { new RecipeRegistration { RecipeName = "Burger and Fries" } });
        }

        public async Task UpdateSessionState(SessionState sessionState)
        {
            Debug.WriteLine("UpdateSessionState");
            await _stateStorage.Store(sessionState, SessionState.SessionKey);
            Debug.WriteLine("UpdateSessionState completed, starting notification");            
        }

        public async Task<SessionState> GetSessionState()
        {
            Debug.WriteLine("GetSessionState");
            var result = await _stateStorage.Get<SessionState>(SessionState.SessionKey);
            if (result == null)
                return new SessionState();
            return result;
        }

        public async Task<SessionState> RegisterOrganisation(string organisationName)
        {
            var sessionState = await GetSessionState();
            var key = $"{Guid.NewGuid()}{Guid.NewGuid()}";
            _registeredOrganisations.Add(key, new OrganisationRegistration { OrganisationName = organisationName });
            _registeredRecipes.Add(key, new List<RecipeRegistration>());
            sessionState.AccessKey = key;
            sessionState.Organisation = _registeredOrganisations[key];
            await UpdateSessionState(sessionState);
            return sessionState;
        }

        public async Task<SessionState> Login(string accessKey)
        {
            var sessionState = await GetSessionState();

            // Mimic login delay
            await Task.Delay(1500);

            sessionState.Organisation = _registeredOrganisations[accessKey];
            sessionState.AccessKey = accessKey;
            sessionState.Recipes = _registeredRecipes[accessKey];
            await UpdateSessionState(sessionState);
            return sessionState;
        }

        public async Task<SessionState> UpdateRecipes()
        {
            var sessionState = await GetSessionState();
            if (sessionState.IsLoggedIn)
            {
                sessionState.Recipes = _registeredRecipes[sessionState.AccessKey];
                await UpdateSessionState(sessionState);
            }
            return sessionState;
        }

        public async Task CreateRecipe(string recipeName, Season season, IngredientCharacteristics ingredientCharacteristics)
        {
            var sessionState = await GetSessionState();
            if (sessionState.IsLoggedIn)
            {
                var recipe = new RecipeRegistration
                {
                    RecipeName = recipeName,
                    IngredientCharacteristics = ingredientCharacteristics,
                    Season = season
                };

                // Mimic some remote call
                await Task.Delay(200);
                _registeredRecipes[sessionState.AccessKey].Add(recipe);                

                // retrieve the recipes
                sessionState.Recipes = _registeredRecipes[sessionState.AccessKey];
            }
        }

        public async Task<IEnumerable<Ingredient>> GetIngredients(RecipeRegistration recipeRegistration)
        {
            Debug.WriteLine("GetIngredients");

            await Task.Delay(10);

            // Get hard coded the ingredients
            if (recipeRegistration.RecipeName == "Burger and Fries")
            {
#if DEBUG
                foreach(var ingredient in _bafIngredients)
                {
                    LogIngredient(ingredient);
                }
#endif

                return _bafIngredients;
            }
            return Enumerable.Empty<Ingredient>();
        }
            

        private void LogIngredient(Ingredient ingredient)
        {
            if (ingredient != null)
            {
                Debug.WriteLine($"Id : {ingredient.IngredientId}");
                Debug.WriteLine($"Name : {ingredient.IngredientName}");
                Debug.WriteLine($"Amount : {ingredient.Amount}");
                Debug.WriteLine($"Amount type : {ingredient.AmountType}");
                Debug.WriteLine($"Ingredient type : {ingredient.IngredientType}");              
            }
        }

        private void LogRecipe(RecipeRegistration registration, string accessKey)
        {
            Debug.WriteLine("Getting recipe details");
            Debug.WriteLine($"RecipeName {registration.RecipeName}");
            Debug.WriteLine($"Acces Key {accessKey}");
            Debug.WriteLine($"Ingredient Characteristics {registration.IngredientCharacteristics}");
            Debug.WriteLine($"Season {registration.Season}");
        }
    }
}
