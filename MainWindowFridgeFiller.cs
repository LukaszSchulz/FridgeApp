using FridgeWPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FridgeWPF
{
    public partial class MainWindow
    {
        public class MainWindowFridgeFiller //uzupełnia stan listy zawartości lodówki na ekranie
        {
            AbstractFridge Fridge;//lodówka, której bedzie dotyczył wypełniacz
            MainWindow mainWindow;//okno zawierające odpowiednią listę
            public MySqlDataBasePuller dataBasePull;//potrzebny do pobierania danych z bazy

            public MainWindowFridgeFiller(AbstractFridge fridge, MainWindow mainWindow, OnlineDataBase odb, string commandString)
            {
                Fridge = fridge;
                this.mainWindow = mainWindow;

                dataBasePull = new MySqlDataBasePuller(odb);//konstruktor inicjalizuje pullera wykorzystując podaną klasę typu OnlineDB
                dataBasePull.PullIngredientsFromDataBase(commandString, Fridge); //automatycznie przekazuje polecenie
            }

            public MainWindowFridgeFiller(AbstractFridge  fridge, MainWindow mainWindow)
            {
                Fridge = fridge;
                this.mainWindow = mainWindow;

                dataBasePull = new MySqlDataBasePuller(mainWindow.DataBase);//konstruktor inicjalizuje pullera inicjalizując 
                                                                            //przy okazji nową bazę danych
                dataBasePull.PullIngredientsFromDataBase(@"SELECT * FROM FridgeContent", Fridge);//konstruktor zawiera 
                               //domyślne polecenie wypełnienia listy składników wszystkimi składnikami obecnymi w bazie
            }

            public void FillFridge()//metoda wypełniająca listę na oknie głównym
            {
                mainWindow.lstFridgeContent.Items.Clear();//czyści zawartość lodówki z pozostających tam obiektów
                foreach (AbstractIngredient ingredient in mainWindow.Fridge.Content)//iteruje przez wszystkie składniki na liście
                {
                    if (ingredient.ExpiryDate > DateTime.Now) //sprawdza, czy data przydatności nie jest zbyt krótka i dokłada
                    {                                         //parametry składnika do listy na oknie głównym
                        ListViewItem item = new ListViewItem();
                        item.Content = $"{ingredient.Name}\n" +
                                       $"{ingredient.Amount}\n" +
                                       $"{ingredient.ExpiryDate}";
                        mainWindow.lstFridgeContent.Items.Add(item);
                    }
                    else
                    {                                           //jeżeli data ważności jest zbyt krótka, zmienia kolor parametrów 
                        ListViewItem item = new ListViewItem(); //wybranego składnika na czerwony i dodaje go do listy na oknie
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
