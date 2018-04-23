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
        public List<AbstractIngredient> FromStringToListOfIngredients(string input)//metoda dopasowująca poszczególne elementy
        {                                                                           //stringa do odpowiednich właściwości
                                                                                    //klasy ingredient
            List<AbstractIngredient> list = new List<AbstractIngredient>();
            string[] listAsArray = input.Split(' ', ';','\n');//metoda rozdziela poszczególne składniki na podstawie wymienionych
                                                                //separatorów
            foreach(string AB in listAsArray)
            {
                string[] recipeAsArray = AB.Split('-');//metoda rozdziela parametry składnika wymienione po myślnikach
                list.Add( FactoryPicker.Instance.Pick(recipeAsArray[0]).Create(Convert.ToDouble(recipeAsArray[1])));
                                                        //tworzony jest składnik, a następnie umieszcza się go na liście
            }
            return list;
        }

        public string FromIngredientToString(AbstractIngredient ingredient)//metoda tworząca string gotowy do umieszczenia na 
        {                                                                   //listBoxie
            string output = ingredient.Name + "-" + ingredient.Amount.ToString();//metoda rozdziela poszczególne składowe składnika
            return output;
        }

    }
}
