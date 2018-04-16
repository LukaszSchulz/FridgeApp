using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public abstract class AbstractRecipe
    {
        public string Name { get; }
        public List<AbstractIngredient> ListOfIngredients { get; protected set; }
        public string Description { get; protected set; }

        public AbstractRecipe(string name)
        {
            Name = name;
            ListOfIngredients = new List<AbstractIngredient>();
        }

        public AbstractRecipe(string name, List<AbstractIngredient> listOfIngredients, string description)
        {
            Name = name;
            ListOfIngredients = listOfIngredients;
            Description = description;
        }

        public void AddIngredient(AbstractIngredient ingredient)
        {
            ListOfIngredients.Add(ingredient);
        }

        public void Describe(string newDescription)
        {
            Description = newDescription;
        }

    }
}
