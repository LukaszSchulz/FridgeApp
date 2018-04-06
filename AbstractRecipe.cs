using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public abstract class AbstractRecipe
    {
        private string Name;
        List<AbstractIngredient> ListOfIngredients;
        private string Description;
        //OnlineDataBase onlineDataBase;

        public AbstractRecipe(string name)
        {
            Name = name;
            ListOfIngredients = new List<AbstractIngredient>();
        }

        public AbstractRecipe(string name, string description)
        {
            Name = name;
            Description = description;
            ListOfIngredients = new List<AbstractIngredient>();
        }

        public abstract void AddIngredients(AbstractIngredient ingredient);

        public List<AbstractIngredient> GetListOfIngredients()
        {
            return ListOfIngredients;
        }

        public string GetRecipeName()
        {
            return Name;
        }

        void AddToDataBase()
        {
            
        }
    }
}
