using System;

namespace JAICT.Blazor.Recipes.Entities
{
    public class RecipeRegistration
    {
        public RecipeRegistration()
        {
            IngredientCharacteristics = Entities.IngredientCharacteristics.All;
            Season = Season.All;           
        }

        public Guid RecipeRegistrationId { get; set; }

        public string RecipeName { get; set; }

        public Season Season { get; set; }

        public IngredientCharacteristics IngredientCharacteristics { get; set; }

        public string EncryptionKey
        {
            get; set;
        }
    }
}
