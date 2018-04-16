using System.Collections.Generic;

namespace FridgeWPF
{
    public class StandardRecipeFactory : AbstractRecipeFactory
    {
        public override AbstractRecipe CreateRecipe(string name, List<AbstractIngredient> listOfIngredients, string description)
        {
            return new StandardRecipe(name, listOfIngredients, description);
        }
    }
}
