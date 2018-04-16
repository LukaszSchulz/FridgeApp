using System.Collections.Generic;

namespace FridgeWPF
{
    public class StandardRecipe : AbstractRecipe
    {
        public StandardRecipe(string name) 
            : base(name)
        {
        }

        public StandardRecipe(string name, List<AbstractIngredient> listOfIngredients, string description) 
            : base(name, listOfIngredients, description)
        {
        }
    }
}
