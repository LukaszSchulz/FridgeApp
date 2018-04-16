using System.Collections.Generic;

namespace FridgeWPF
{
    public abstract class AbstractRecipeFactory
    {
        public abstract AbstractRecipe CreateRecipe(string name, List<AbstractIngredient> listOfIngredients, 
                                                        string description);
    }
}
