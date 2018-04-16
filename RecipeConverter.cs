using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public class RecipeConverter //zawiera metody konwertujące przepisy z klasy AbstractRecipe na string i odwrotnie 
                                        //(w celu dopasowania ich do struktury bazy danych)
    {
        private List<AbstractIngredient> list;

        public List<AbstractIngredient> FromStringToListOfIngredients(string input)
        {
            list = new List<AbstractIngredient>();
            string[] listAsArray = input.Split(' ', ';','\n');
            
            foreach(string AB in listAsArray)
            {
                string[] recipeAsArray = AB.Split('-');
                list.Add( FactoryPicker.Instance.Pick(recipeAsArray[0]).Create(Convert.ToDouble(recipeAsArray[1])));
            }
            return list;
        }

        public string FromIngredientToString(AbstractIngredient ingredient)
        {
            string output = ingredient.Name + "-" + ingredient.Amount.ToString();
            return output;
        }

    }
}
