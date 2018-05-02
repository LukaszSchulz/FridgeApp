using FridgeWPF;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FridgeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AbstractFridge Fridge { get; private set; } //umożliwia dostęp do lodówki i jej metod
        public OnlineDataBase DataBase { get; internal set; }

        public MainWindow()
        {
            InitializeComponent();
            Options options = new Options(this);//inicjalizuje okno, w którym można wybrać odpowiednie połączenie
            options.Show();//otwiera okno opcji
            IsEnabled = false;//blokuje okno główne do czasu ustawienia bazy danych
            //RefreshPage();  //wykorzystuje lokalną metodę do wypełnienia wszystkich pól na oknie głównym
        }

        //region z wydarzeniami wykonywanymi po naciśnięciu wybranych przycisków
        #region Button handling

        private void BtnRefreshFridgeContent_Click(object sender, RoutedEventArgs e)
        {
            RefreshPage();//pobiera na nowo dane do wypełnienia list i odświeża widok
        }

        private void BtnAddNewIngredient_Click(object sender, RoutedEventArgs e) //metoda umożliwiająca dodanie nowych składników
        {                                                                       //do lodówki przez formularz na oknie głównym
            try
            {
                Fridge.AddNewIngredientToDatabase(Fridge.Filler.dataBasePull.DataBase,          //wykorzystuje metodę dodającą nowy
                                                  cmbIngredientList.SelectedValue.ToString(),   //składnik, należącą do obiektu
                                                  Convert.ToDouble(txtInputAmount.Text),        //typu fridge, wykorzystując pola
                                                  DateTime.Parse(cldExpiryDate.Text));          //wypełnione w formularzu
                MessageBox.Show("Ingredient added successfully!", "Success!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Adding failed", "MainWindow.BtnAddNew", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            RefreshPage();  //na koniec odświeża widok, żeby załadować nowe dane z bazy i zaktualizować listę w lodówce
        }

        private void BtnDeleteIngredient_Click(object sender, RoutedEventArgs e)//metoda usuwająca wybrany składnik z bazy danych
        {                                                                       //polega na porównianiu parametrów wybranego 
                                                                                //obiektu z listy z obiektami w bazie danych
            try
            {
                string[] Ingredient = lstFridgeContent.SelectedItem.ToString().Split(' ', '\n');//rozbija tekst z listy na 
                                                                                                //pojedyńcze informacje
                AbstractIngredient ingredient = FactoryPicker.Instance.Pick(Ingredient[1]).     //dopasowuje dane do odpowiednich
                                                Create(Double.Parse(Ingredient[2]),             //właściwości klasy ingredient
                                                Convert.ToDateTime(Ingredient[3]));
                Fridge.DeleteIngredientFromDataBase(Fridge.Filler.dataBasePull.DataBase, ingredient); //używa metody Delete klasy   
                                                                                                        //fridge
                MessageBox.Show("Thrown out!", "Success!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Deleting failed", "MainWindow.BtnDelete",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            RefreshPage();  //odświeża widok
        }

        private void BtnOpenRecipeBook_Click(object sender, RoutedEventArgs e)//otwiera okno obsługujące dostęp do przepisów
        {
            WindowRecipeBook windowRecipeBook = new WindowRecipeBook(this);//tworzy nową instancję okna z przepisami
            windowRecipeBook.Show();              //otwiera okno z obsługą przepisów
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)//otwiera okno ustawień, w którym można ustawić nową 
        {                                                              //bazę danych, z którą ma się łączyć aplikacja
            Options options = new Options(this);//tworzy nową instancję okna
            options.Show();//otwiera okno
        }

        #endregion Button handling

        private void FillComboBox()//metoda wypełniająca comboBox nazwami zaprogramowanych składników
        {
            foreach(AbstractIngredientFactory IF in FactoryPicker.Instance.listOfFactories)//wybierana jest każda nazwa fabryki,
            {                                                                               //które odpowiadają składnikom
                cmbIngredientList.Items.Add(IF.Name);
            }
        }

        public void RefreshPage()//metoda odświeżająca ekran
        {
            cmbIngredientList.Items.Clear();//usuwa wszystkie obiekty z listy składników
            txtInputAmount.Text = "";//czyści pole tekstowe formularza dotyczące ilości
            cldExpiryDate.Text = DateTime.Now.ToString();//czyści pole tekstowe formularza dotyczące daty
            FillComboBox();//wypełnia comboBox
            Fridge = new StandardFridge(this);//odnawia instancję lodówki
            Fridge.Filler.FillFridge();//wykorzystuje metodę FillFridge obiektu Filler należącego do lodówki,
                                        //do wypełnienia listy na ekranie głównym
        }
    }
}
