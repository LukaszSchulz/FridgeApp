using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FridgeWPF
{
    public class StateChecker // dostarcza narzędzia do porównywania stanu lodówki z listą składników z przepisu
    {
        protected AbstractFridge Fridge { get; private set; }//umożliwia dostęp do lodówki

        public StateChecker(AbstractFridge fridge)
        {
            Fridge = fridge;
        }

        public List<AbstractIngredient> EmptyListOfIngredients //lista nazw składników, na podstawie której powstanie lista docelowa
        {
            get
            {
                List<AbstractIngredient> EmptyIngredientList = new List<AbstractIngredient>(); 
                foreach (AbstractIngredientFactory f in FactoryPicker.Instance.listOfFactories)//wykorzystuje dostępną listę fabryk
                                                                                               //do stworzenia listy nazw
                {
                    string factoryName = f.Name;
                    EmptyIngredientList.Add(FactoryPicker.Instance.Pick(factoryName).Create(0));//tworzy puste instancje składników,
                                                                                                // do wypełniania przez zawartość
                                                                                                //bazy danych
                }
                return EmptyIngredientList;
            }
        }

        private List<AbstractIngredient> SumFridgeIngredients() // tworzy i zwraca listę unikalnych produktów 
                                                                //ze zsumowanymi ilościami
        {
            List<AbstractIngredient> ListOfUniqueIngredients = EmptyListOfIngredients; //lokalna lista, której składniki będą
                                                                                       //uzupełniane o ilości
            foreach(AbstractIngredient i in Fridge.Content)
            {
                foreach(AbstractIngredient Unique in ListOfUniqueIngredients)
                {
                    if (i.Name == Unique.Name) //sprawdza, czy nazwy składników się zgadzają i jeśli tak, 
                                                //to uzupełnia ich właściwość Amount
                    {
                        Unique.AddAmount(i.Amount);
                    }
                }
            }
            return ListOfUniqueIngredients;
        }

        public bool Check(AbstractRecipe recipe) //porównuje ilości dostępnych składników z tymi, 
                                                    //które są potrzebne w przepisie
        {
            List<AbstractIngredient> AvailibleIngredients = SumFridgeIngredients();
            int Count = recipe.ListOfIngredients.Count;//przechowuje ilość składników, 
                                                        //które jeszcze zostały do odnalezienia i porówniana

            foreach (AbstractIngredient recipeIngredient in recipe.ListOfIngredients)
            {
                for(int i = 0; i< AvailibleIngredients.Count; i++)
                {
                    if(AvailibleIngredients[i].Name == recipeIngredient.Name)
                    {
                        if (AvailibleIngredients[i].Amount < recipeIngredient.Amount)//jeśli okazuje się, 
                                                                                     //że któregokolwiek składnika jest za mało, 
                                                                                     //pętla jest przerywana i metoda zwraca false
                        {
                            return false;
                        }
                        else
                        {
                            Count -= 1;//jeśli ilość się zgadza, odznacza się, że jeden ze składników został znaleziony
                        }
                    }
                    if (Count <= 0) return true;//tylko w przypadku, kiedy wszystkie składniki zostały znalezione,
                                                //metoda zwraca true
                }
            }
            return false;
        }
    }
}