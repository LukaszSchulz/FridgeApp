using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FridgeWPF
{
    public class FoodPuller // służy do operacji związanych z wybieraniem z magazynu/lodówki 
                            //produktów potrzebnych do przepisu
    {
        AbstractFridge Fridge;
        DataBasePuller DataPuller;

        public FoodPuller(AbstractFridge fridge, DataBasePuller dataPuller)
        {
            Fridge = fridge;
            DataPuller = dataPuller;
        }

        public void PullAllForRecipe(AbstractRecipe recipe) // wyciąga z magazynu/lodówki wszyskie składniki z listy
        {
            string message = "";
            if (Fridge.StateCheck.Check(recipe))
            {
                try
                {
                    foreach(AbstractIngredient ingredient in recipe.ListOfIngredients)
                    {
                        PullIngredientFromFridge(ingredient);
                        message += $"{ingredient.Amount} {ingredient.Unit} of {ingredient.Name} taken.\n";
                    }
                    MessageBox.Show(message);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,  "FoodPuller.PullAllRecipes");
                }
            }
        }

        private void PullIngredientFromFridge(AbstractIngredient neededIngredient) // pobiera z lodówki odpowiednią ilość 
                                                                            //najstarszego produktu i wkłada go na miejsce;
        {
            try
            {
                AbstractIngredient NeededIngredient = neededIngredient;
                AbstractIngredient oldestIngredient = FindOldestIngredient(neededIngredient);
                AbstractIngredient putBack;//tu wyląduje niewykorzystana resztka

                double amountToTake = neededIngredient.Amount;

                if (amountToTake < oldestIngredient.Amount)
                {
                    putBack = FactoryPicker.Instance.Pick(oldestIngredient.Name).
                                            Create(oldestIngredient.Amount - amountToTake,
                                            oldestIngredient.ExpiryDate);
                    Fridge.DeleteIngredientFromDataBase(new FreeSqlDataBase(), oldestIngredient);
                    Fridge.AddNewIngredientToDatabase(new FreeSqlDataBase(), putBack);
                }
                else if (amountToTake == oldestIngredient.Amount)
                {
                    Fridge.DeleteIngredientFromDataBase(new FreeSqlDataBase(), oldestIngredient);
                }
                else if (amountToTake > oldestIngredient.Amount)
                //jeśli jedno opakowanie to za mało, to metoda zostanie wykonana jeszcze raz
                {
                    NeededIngredient.TakeAmount(oldestIngredient.Amount);
                    Fridge.DeleteIngredientFromDataBase(new FreeSqlDataBase(), oldestIngredient);
                    PullIngredientFromFridge(NeededIngredient);
                }
            }
            catch(MySqlException ex) { MessageBox.Show(ex.Message, "FoodPuller.PullAllIngredients"); }
            catch(Exception ex) { MessageBox.Show(ex.Message, "FoodPuller.PullAllIngredients"); }
        }

        private AbstractIngredient FindOldestIngredient(AbstractIngredient ingredient) 
                                                    //wyszukuje składnika z najstarszą datą ważności
        {
            AbstractIngredient oldest = ingredient;
            foreach (AbstractIngredient AI in Fridge.GetContent())
            {
                if(AI.Name == ingredient.Name)
                {
                    oldest = AI;
                }
            }
            for(int i = 1; i < Fridge.GetContent().Count; i++)
            {
                if (Fridge.GetContent()[i].Name == ingredient.Name)
                {
                    if (oldest.ExpiryDate > Fridge.GetContent()[i].ExpiryDate)
                    {
                        oldest = Fridge.GetContent()[i];
                    }
                }
            }
            return oldest;
        }
    }
}
