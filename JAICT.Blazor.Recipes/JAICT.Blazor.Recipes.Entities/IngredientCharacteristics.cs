using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JAICT.Blazor.Recipes.Entities
{
    public enum IngredientCharacteristics
    {
        [Description("The recipe can contain any kind of ingredients")]
        All = 0,

        [Description("The recipe is a typical meat dish")]
        Meat = 1,

        [Description("The recipe is a typical fish dish")]
        Fish = 2,

        [Description("The recipe is a vegetarion dish")]
        Vegetarian = 2,

        [Description("The recipe is a vegan dish")]
        Vegan = 2
    }
}
