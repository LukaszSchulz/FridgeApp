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
        AbstractFridge Fridge; //pole na lodówkę, z której mają być wybierane składniki
        MySqlDataBasePuller DataPuller;//umożliwia usuwanie z bazy danych wybranych składników

        public FoodPuller(AbstractFridge fridge, MySqlDataBasePuller dataPuller)
        {
            Fridge = fridge;
            DataPuller = dataPuller;
        }

        public void PullAllForRecipe(AbstractRecipe recipe) // wyciąga z magazynu/lodówki wszyskie składniki z listy
        {
            if (Fridge.StateCheck.Check(recipe))//przy użyciu metody Check klasy stateCheck sprawdza, czy w lodówce jest 
                                                //wystarczająco dużo składników na wybrany przepis, jeśli tak, to kontynuuje
            {
                try
                {
                    foreach(AbstractIngredient ingredient in recipe.ListOfIngredients)//wyciąga każdy składnik po kolei korzystając
                    {                                                               //z lokalnej metody PullIngredientFromFridge
                        PullIngredientFromFridge(ingredient);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void PullIngredientFromFridge(AbstractIngredient neededIngredient) // pobiera z lodówki odpowiednią ilość 
                                                                                   //najstarszego produktu i wkłada go na miejsce                                                                            
        {
            try
            {
                AbstractIngredient NeededIngredient = neededIngredient; //pole, którego wartość może być zmniejszana
                AbstractIngredient oldestIngredient = FindOldestIngredient(neededIngredient);//pole, w którym znajduje się składnik 
                                                                                                //uznawany za najstarszy
                AbstractIngredient putBack;                 //tu wyląduje niewykorzystana resztka, którą następnie lokuje się 
                                                                //spowrotem w bazie danych
                double amountToTake = neededIngredient.Amount;  //pole na ilość danego składnika do wzięcia - zmniejsza się wraz 
                                                                // z wyjmowaniem kolejnych produktów danego typu
                if (amountToTake < oldestIngredient.Amount)
                {
                    putBack = FactoryPicker.Instance.Pick(oldestIngredient.Name).       //wypełnia pole ze składnikiem do odłożenia
                                            Create(oldestIngredient.Amount - amountToTake,
                                            oldestIngredient.ExpiryDate);
                    Fridge.DeleteIngredientFromDataBase(Fridge.Window.DataBase, oldestIngredient);//usuwa z bazy wzięty składnik
                    Fridge.AddNewIngredientToDatabase(Fridge.Window.DataBase, putBack);//dodaje w miejsce usuniętego 
                                                                                     //składnika składnik o zmniejszonej ilości
                }
                else if (amountToTake == oldestIngredient.Amount) //jeśli ilość najstarszego składnika pokrywa się
                                                                    //z jego wymaganą ilością, to jest usuwany w całości
                {
                    Fridge.DeleteIngredientFromDataBase(Fridge.Window.DataBase, oldestIngredient);
                }
                else if (amountToTake > oldestIngredient.Amount)
                //jeśli jedno "opakowanie" to za mało, to metoda zostanie wykonana jeszcze raz
                {
                    NeededIngredient.TakeAmount(oldestIngredient.Amount); //zmniejsza wartość amount pola NeededIngredient
                    Fridge.DeleteIngredientFromDataBase(Fridge.Window.DataBase, oldestIngredient);//usuwa wykorzystany składnik z BD
                    oldestIngredient = FindOldestIngredient(neededIngredient); // odnajduje kolejny najstarszy składnik
                    PullIngredientFromFridge(NeededIngredient);//kontynuuje pobieranie z "lodówki" ze zmniejszonym wymaganiem
                }
            }
            catch (MySqlException ex) { MessageBox.Show(ex.Message, "FoodPuller.PullAllIngredients"); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "FoodPuller.PullAllIngredients"); }
            
        }
        
        private AbstractIngredient FindOldestIngredient(AbstractIngredient neededIngredient) 
                                                    //wyszukuje składnika z najstarszą datą ważności
        {
            AbstractIngredient oldest = neededIngredient; //pole w którym będzie zapisywany najstarszy składnik
            foreach (AbstractIngredient AI in Fridge.Content)//iteruje przez wszyskie składniki w lodówce
            {
                if(AI.Name == neededIngredient.Name)    //w polu zapisany zostaje pierwszy składnik o nazwie takiej jak poszukiwany
                {
                    oldest = AI;
                    break;
                }
            }
            for(int i = 1; i < Fridge.Content.Count; i++)//każda pozycja w lodówce jest sprawdzana pod kątem nazwy i jeżeli jej
            {                                           //nazwa się zgadza, a data ważności jest starsza niż dotychczasowa, 
                                                        //to starszy składnik zastępuje dotychczasowy
                if (Fridge.Content[i].Name == neededIngredient.Name)
                {
                    if (oldest.ExpiryDate > Fridge.Content[i].ExpiryDate)
                    {
                        oldest = Fridge.Content[i];
                    }
                }
            }
            return oldest;
        }
    }
}
