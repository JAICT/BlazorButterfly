using System;
using System.Collections.Generic;
using System.Text;

namespace JAICT.Blazor.Recipes.Entities
{
    public enum IngredientType
    {
        /// <summary>
        /// This ingredient is usually available in stock in a pantry
        /// </summary>
        Stock = 0,
        
        /// <summary>
        /// The ingredient must usually be bought fresh
        /// </summary>
        Fresh = 1,
        
        /// <summary>
        /// The type of the ingredient is unknown
        /// </summary>
        Unknown = 2
    }
}
