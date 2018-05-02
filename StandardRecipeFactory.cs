using System.Collections.Generic;

namespace FridgeWPF
{
    public class StandardRecipeFactory : AbstractRecipeFactory //konkretna fabryka składników o braku dodatkowych właściwości
    {
        public override AbstractRecipe CreateRecipe(string name, List<AbstractIngredient> listOfIngredients, string description)
        {
            return new StandardRecipe(name, listOfIngredients, description);
        }
    }
}
