using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace FridgeWPF
{
    /// <summary>
    /// Interaction logic for RecipeBook.xaml
    /// </summary>
    public partial class WindowRecipeBook : Window, IRecipeBook //Okno obsługujące listę z przepisami
    {
        MainWindow window;              // na podstawie okna głównego RecipeBook może korzystać z instancji lodówki
        public List<AbstractRecipe> Recipebook { get; } = new List<AbstractRecipe>();//lista przepisów wymagana przez interfejs
        MySqlDataBasePuller puller;//umożliwia dostęp do pobierania danych z bazy danych

        public WindowRecipeBook(MainWindow window)
        {
            InitializeComponent();
            RefreshRecipeBook();
            this.window = window;
        }
        
        private void lstRecipeBook_SelectionChanged(object sender, SelectionChangedEventArgs e) //wypełnia listę składnikami
                                                                                                // na wybrany przepis z listy
        {
            try
            {
                if (lstRecipeBook.SelectedItem != null) //sprawdza, czy na pewno został wybrany któryś z przepisów
                {
                    lstChosenRecipe.Items.Clear();
                    foreach (AbstractRecipe AR in Recipebook)
                    {

                        if (AR.Name == lstRecipeBook.SelectedItem.ToString())
                        {
                            lstChosenRecipe.Items.Add(AR.Name);

                            foreach (AbstractIngredient AI in AR.ListOfIngredients)
                            {
                                lstChosenRecipe.Items.Add(AI.Name + " " + AI.Amount + " " + AI.Unit); 
                                                                    //tworzy wpis na listę składników
                            }
                            lstChosenRecipe.Items.Add(AR.Description);//dodaje opis do przepisu
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            NewRecipe newRecipe = new NewRecipe(window.DataBase);
            newRecipe.Show();
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (lstRecipeBook.SelectedItem != null)
            {
                foreach (AbstractRecipe AR in Recipebook)
                {
                    if (AR.Name == lstRecipeBook.SelectedItem.ToString())
                    {
                        if (window.Fridge.IsThereEnough(AR))
                        {
                            MessageBox.Show($"You have enough ingredients to make {AR.Name}!");
                        }
                        else
                        {
                            MessageBox.Show($"You do not have enough ingredients!");
                        }
                    }
                }
            }
        }

        private void btnOnlyAvailible_Click(object sender, RoutedEventArgs e) //wyświetla na liście przepisów tylko te, 
                                                                            //dla których jest wystarczająca ilość składników
        {
            Recipebook.Clear(); //robi miejsce na wyselekcjonowane przepisy
            puller = new MySqlDataBasePuller(window.DataBase);//dostarcza metody potrzebne do pobierania danych
            puller.PullRecipesFromDatabase("SELECT * FROM Recipes", this);//wybiera które przepisy mają być przejrzane 
            lstRecipeBook.Items.Clear();
            foreach (AbstractRecipe recipe in Recipebook)
            {
                if (window.Fridge.IsThereEnough(recipe) == true)
                {
                    lstRecipeBook.Items.Add(recipe.Name);
                }
            }
        }

        private void btnUseRecipe_Click(object sender, RoutedEventArgs e) //wybiera przepis do wykonania na podstawie 
                                                                        //selekcji z listy
        {
            FoodPuller foodPuller = new FoodPuller(window.Fridge, puller); //klasa dzięki której możliwe jest usuwanie 
                                                                            //wybranych składników z bazy danych
            if (lstRecipeBook.SelectedItem != null)
            {
                foreach (AbstractRecipe recipe in Recipebook)
                {
                    if (recipe.Name == lstRecipeBook.SelectedItem.ToString())
                    {
                        foodPuller.PullAllForRecipe(recipe);//usuwa z lodówki składniki potrzebne do przepisu, po uprzednim sprawdzeniu,
                                                            // czy jest ich wystarczająca ilość
                        window.RefreshPage(); //odświeża widok na oknie ze składnikami
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose a recipe first!");
            }
            RefreshRecipeBook();
        }

        private void btnShowAllRecipes_Click(object sender, RoutedEventArgs e) // odświeża widok "książki z przepisami"
        {
            RefreshRecipeBook();
        }

        private void RefreshRecipeBook() // metoda odświeżająca widok dla przycisku ShowAllRecipes
        {
            Recipebook.Clear();
            puller = new MySqlDataBasePuller(window.DataBase);//umożliwia pobieranie informacji o przepisach i składnikach z bazy
            puller.PullRecipesFromDatabase("SELECT * FROM Recipes", this); // wypełnia listę przepisów dostarczoną przez interfejs 
            lstRecipeBook.Items.Clear();//usuwa wszystkie przedmioty z listy, żeby się nie dublowały
            foreach (AbstractRecipe recipe in Recipebook) //wypełnia listView nazwami przepisów
            {
                lstRecipeBook.Items.Add(recipe.Name);
            }
        }
    }
}
