using FridgeWPF;

namespace FridgeWPF
{
    public partial class MainWindow
    {
        public class FridgeFiller
        {
            AbstractFridge Fridge;
            MainWindow mainWindow;
            public DataBasePull dataBasePull;

            public FridgeFiller(AbstractFridge  fridge, MainWindow mainWindow)
            {
                Fridge = fridge;
                this.mainWindow = mainWindow;

                dataBasePull = new DataBasePull(new FreeSqlDataBase(), @"SELECT * FROM FridgeContent", fridge);
                dataBasePull.PullDataFromDataBase();
            }

            public void FillFridge()
            {
                mainWindow.lstFridgeContent.Items.Clear();
                foreach (AbstractIngredient ingredient in mainWindow.fridge.GetContent())
                {
                    mainWindow.lstFridgeContent.Items.Add($" {ShortName(ingredient.Name)} \n" +
                                                $" {ingredient.Amount} \n" +
                                                $" {ingredient.ExpiryDate}");
                }
            }

            string ShortName(string longName)
            {
                string shortName;

                string[] dividedName = longName.Split('.');

                shortName = dividedName[(dividedName.Length - 1)];

                return shortName;
            }
        }
    }
}
