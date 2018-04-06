using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public class NewIngredientSaver //zajmuje się dodawaniem nowych obiektów do bazy danych lodówki
    {
        OnlineDataBase DataBase;

        public NewIngredientSaver(OnlineDataBase dataBase)
        {
            DataBase = dataBase;
        }

        public void AddNewIngredient(string ingredientName, double amount, DateTime expiryDate)
        {
            AbstractIngredient newIngredient = FactoryPicker.Instance.Pick(ingredientName).Create(amount, expiryDate);
            DataBase.AddIngredientToDatabase(newIngredient);
        }
    }
}
