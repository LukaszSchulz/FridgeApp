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
        public AbstractFridge fridge { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            RefreshPage();
        }

        private void BtnRefreshFridgeContent_Click(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }

        private void BtnAddNewIngredient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fridge.AddNewIngredientToDatabase(fridge.Filler.dataBasePull.DataBase,
                                                  cmbIngredientList.SelectedValue.ToString(),
                                                  Convert.ToDouble(txtInputAmount.Text),
                                                  DateTime.Parse(cldExpiryDate.Text));
                MessageBox.Show("Ingredient added successfully!", "Success!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Adding failed", "MainWindow.BtnAddNew", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            RefreshPage();
        }

        private void FillComboBox()
        {
            foreach(AbstractIngredientFactory IF in FactoryPicker.Instance.listOfFactories)
            {
                cmbIngredientList.Items.Add(IF.Name);
            }
        }

        public void RefreshPage()
        {
            cmbIngredientList.Items.Clear();
            txtInputAmount.Text = "";
            cldExpiryDate.Text = DateTime.Now.ToString();
            FillComboBox();
            fridge = new StandardFridge(this);
            fridge.Filler.FillFridge();
        }

        private void BtnDeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] Ingredient = lstFridgeContent.SelectedItem.ToString().Split(' ','\n');
                AbstractIngredient ingredient = FactoryPicker.Instance.Pick(Ingredient[0]). 
                                                Create(Double.Parse(Ingredient[1]),
                                                Convert.ToDateTime(Ingredient[2]));
                fridge.DeleteIngredientFromDataBase(fridge.Filler.dataBasePull.DataBase, ingredient);
                MessageBox.Show("Thrown out!", "Success!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Deleting failed", "MainWindow.BtnDelete",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            RefreshPage();
        }

        private void BtnOpenRecipeBook_Click(object sender, RoutedEventArgs e)
        {
            WindowRecipeBook windowRecipeBook = new WindowRecipeBook(this);
            windowRecipeBook.Show();
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            Options options = new Options(this);
            options.Show();
        }
    }
}
