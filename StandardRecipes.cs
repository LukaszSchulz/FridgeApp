using System.Collections.Generic;

namespace FridgeWPF
{
    public class StandardRecipe : AbstractRecipe //konkretny składnik, bez dodatkowych właściwości
    {
        public StandardRecipe(string name, List<AbstractIngredient> listOfIngredients, string description) 
            : base(name, listOfIngredients, description)
        {
        }
    }
}
