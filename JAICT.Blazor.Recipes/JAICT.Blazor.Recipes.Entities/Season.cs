using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.Entities
{
    public enum Season
    {
        [Description("The meal is appropriate for all seasons.")]
        All = 0,

        [Description("The meal is a typical winter meal.")]
        Winter = 1,

        [Description("The meal is typical for spring time.")]
        Spring = 2,

        [Description("This is a typical summer meal.")]
        Summer = 3,

        [Description("Autumn is best for creating this dish.")]
        Autumn = 4
    }
}
