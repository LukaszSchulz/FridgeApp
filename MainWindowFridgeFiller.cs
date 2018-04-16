using FridgeWPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FridgeWPF
{
    public partial class MainWindow
    {
        public class FridgeFiller //uzupełnianie stanu lodówki na ekranie
        {
            AbstractFridge Fridge;
            MainWindow mainWindow;
            public DataBasePuller dataBasePull;

            public FridgeFiller(AbstractFridge  fridge, MainWindow mainWindow)
            {
                Fridge = fridge;
                this.mainWindow = mainWindow;

                dataBasePull = new DataBasePuller(new FreeSqlDataBase());
                dataBasePull.PullIngredientsFromDataBase(@"SELECT * FROM FridgeContent", fridge);
            }

            public void FillFridge()
            {
                mainWindow.lstFridgeContent.Items.Clear();
                foreach (AbstractIngredient ingredient in mainWindow.fridge.GetContent())
                {
                    if (ingredient.ExpiryDate < DateTime.Now)
                    {
                        mainWindow.lstFridgeContent.Items.Add($"{ingredient.Name}\n" +
                                                $"{ingredient.Amount}\n" +
                                                $"{ingredient.ExpiryDate}");
                    }
                    else
                    {
                        ListViewItem item = new ListViewItem();
                        item.Foreground = Brushes.Red;
                        item.Content = $"{ingredient.Name}\n" +
                                        $"{ingredient.Amount}\n" +
                                        $"{ingredient.ExpiryDate}";
                        mainWindow.lstFridgeContent.Items.Add(item);                        
                    }
                }
            }
        }
    }
}
