using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public class NewIngredientSaver //kapsułkuje dodawanie nowych obiektów do bazy danych lodówki, umożliwiając dodanie nowych 
    {                               //składników tylko przez podanie odpowiednich parametrów
        OnlineDataBase DataBase;  //zapewnia dostęp do bazy danych

        public NewIngredientSaver(OnlineDataBase dataBase)
        {
            DataBase = dataBase;
        }

        public void AddNewIngredient(string ingredientName, double amount, DateTime expiryDate)//metoda pozwalająca na dodanie 
        {                                                                                      //nowych składników do bazy danych
            AbstractIngredient newIngredient = FactoryPicker.Instance.Pick(ingredientName).Create(amount, expiryDate);
                                                                   //pomaga określić rodzaj składnika i od razu do instancjonuje
            DataBase.AddIngredientToDatabase(newIngredient);//dodaje nowy składnik w postaci klasy do bazy danych
        }
    }
}
