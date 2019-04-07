using System;

namespace JAICT.Blazor.Recipes.Entities
{
    public class Ingredient
    {
        public Guid IngredientId { get; set; }

        public string Description { get; set; }

        public string IngredientName { get; set; }

        public int Amount { get; set; }

        public string AmountType { get; set; }

        public IngredientType IngredientType { get; set; }
    }
}
