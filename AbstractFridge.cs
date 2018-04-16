using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FridgeWPF.MainWindow;

namespace FridgeWPF
{
    public abstract class AbstractFridge
    {
        public FridgeFiller Filler;
        private NewIngredientSaver NewIngredient;
        List<AbstractIngredient> Content = new List<AbstractIngredient>();
        public StateChecker StateCheck { get; private set; }
        MainWindow Window;

        public AbstractFridge(MainWindow window)
        {
            Filler = new FridgeFiller(this, window);
            StateCheck = new StateChecker(this);
            Window = window;
        }

        public List<AbstractIngredient> GetContent()
        {
            return Content;
        }

        public void AddIngredient(AbstractIngredient ingredient)
        {
            Content.Add(ingredient);
        }

        public void AddNewIngredientToDatabase(OnlineDataBase dataBase, string ingredientName, 
                                                double amount, DateTime expiryDate)
        {
            NewIngredient = new NewIngredientSaver(dataBase);
            NewIngredient.AddNewIngredient(ingredientName, amount, expiryDate);
        }

        public void AddNewIngredientToDatabase(OnlineDataBase dataBase, AbstractIngredient ingredient)
        {
            NewIngredient = new NewIngredientSaver(dataBase);
            NewIngredient.AddNewIngredient(ingredient.Name,ingredient.Amount, ingredient.ExpiryDate);
        }

        public void DeleteIngredientFromDataBase(OnlineDataBase dataBase, AbstractIngredient ingredient)
        {
            dataBase.DeleteIngredientFromDatabase(ingredient);
        }

        public bool IsThereEnough(AbstractRecipe recipe)
        {
            //StateCheck = new StateChecker(this);
            return StateCheck.Check(recipe);
        }
    }
}
