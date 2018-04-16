using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FridgeWPF
{
    public class StateChecker // służy do porównywania stanu lodówki z listą składników z przepisu
    {
        protected AbstractFridge Fridge { get; private set; }

        public StateChecker(AbstractFridge fridge)
        {
            Fridge = fridge;
        }

        private List<AbstractIngredient> SumFridgeIngredients() // tworzy listę unikalnych produktów ze zsumowanymi ilościami
        {
            List<AbstractIngredient> list = Fridge.GetContent();
            
            for(int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < Fridge.GetContent().Count; j++)
                {
                    if (i != j)
                    {
                        if (list[i].Name == Fridge.GetContent()[j].Name)
                        {
                            list[i].AddAmount(Fridge.GetContent()[j].Amount);
                            list.RemoveAt(j);
                        }
                    }
                }
            }
            return list;
        }

        public bool Check(AbstractRecipe recipe) //porównuje ilości dostępnych składników z tymi, 
                                                    //które są potrzebne w przepisie
        {
            List<AbstractIngredient> AvailibleIngredients = SumFridgeIngredients();
            int Count = recipe.ListOfIngredients.Count;

            foreach (AbstractIngredient recipeIngredient in recipe.ListOfIngredients)
            {
                for(int i = 0; i< AvailibleIngredients.Count; i++)
                {
                    if(AvailibleIngredients[i].Name == recipeIngredient.Name)
                    {
                        if (AvailibleIngredients[i].Amount < recipeIngredient.Amount)
                        {
                            return false;
                        }
                        else
                        {
                            Count -= 1;
                        }
                    }
                    if (Count <= 0) return true;
                }
            }
            return false;
        }
    }
}