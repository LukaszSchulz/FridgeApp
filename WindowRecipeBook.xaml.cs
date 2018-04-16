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
    public partial class WindowRecipeBook : Window, IRecipeBook
    {
        MainWindow window;
        public List<AbstractRecipe> Recipebook { get; } = new List<AbstractRecipe>();
        DataBasePuller puller;

        public WindowRecipeBook(MainWindow window)
        {
            InitializeComponent();
            RefreshRecipeBook();
            this.window = window;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshRecipeBook();
        }

        private void RefreshRecipeBook()
        {
            Recipebook.Clear();
            puller = new DataBasePuller(new FreeSqlDataBase());
            puller.PullRecipesFromDatabase("SELECT * FROM Recipes", this);
            lstRecipeBook.Items.Clear();
            foreach(AbstractRecipe recipe in Recipebook)
            {
                lstRecipeBook.Items.Add(recipe.Name);
            }
        }
        
        private void lstRecipeBook_SelectionChanged(object sender, SelectionChangedEventArgs e) //wypełnia listę składnikami
                                                                                                // na wybrany przepis
        {
            lstChosenRecipe.Items.Clear();

            try
            {
                foreach (AbstractRecipe AR in Recipebook)
                {
                    if (AR.Name == lstRecipeBook.SelectedItem.ToString())
                    {
                        lstChosenRecipe.Items.Add(AR.Name);

                        foreach(AbstractIngredient AI in AR.ListOfIngredients)
                        {
                            lstChosenRecipe.Items.Add(AI.Name + " " + AI.Amount + " " + AI.Unit);
                        }

                        lstChosenRecipe.Items.Add(AR.Description);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            NewRecipe newRecipe = new NewRecipe(new FreeSqlDataBase());
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
                        if (window.fridge.IsThereEnough(AR))
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

        private void btnOnlyAvailible_Click(object sender, RoutedEventArgs e)
        {
            Recipebook.Clear();
            puller = new DataBasePuller(new FreeSqlDataBase());
            puller.PullRecipesFromDatabase("SELECT * FROM Recipes", this);
            lstRecipeBook.Items.Clear();
            foreach (AbstractRecipe recipe in Recipebook)
            {
                if (window.fridge.IsThereEnough(recipe) == true)
                {
                    lstRecipeBook.Items.Add(recipe.Name);
                }
            }
        }

        private void btnUseRecipe_Click(object sender, RoutedEventArgs e) //wybiera przepis do wykonania na podstawie 
                                                                        //selekcji z listy
        {
            FoodPuller foodPuller = new FoodPuller(window.fridge, puller);

            if (lstRecipeBook.SelectedItem != null)
            {
                foreach (AbstractRecipe recipe in Recipebook)
                {
                    if (recipe.Name == lstRecipeBook.SelectedItem.ToString())
                    {
                        foodPuller.PullAllForRecipe(recipe);
                        window.RefreshPage();
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose a recipe first!");
            }
        }
    }
}
