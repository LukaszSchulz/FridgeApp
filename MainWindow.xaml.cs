using FridgeWPF;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FridgeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AbstractFridge fridge;

        public MainWindow()
        {
            InitializeComponent();
            RefreshPage();
        }

        private void btnRefreshFridgeContent_Click(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }

        private void btnAddNewIngredient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fridge.AddNewIngredientToDatabase(fridge.Filler.dataBasePull.DataBase,
                                                  cmbIngredientList.SelectedValue.ToString(),
                                                  Convert.ToDouble(txtInputAmount.Text),
                                                  DateTime.Parse(cldExpiryDate.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Adding failed", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            RefreshPage();
        }

        private void FillComboBox()
        {
            foreach(IngredientFactory IF in FactoryPicker.Instance.listOfFactories)
            {
                cmbIngredientList.Items.Add(IF.name);
            }
        }

        void RefreshPage()
        {
            cmbIngredientList.Items.Clear();
            txtInputAmount.Text = "";
            cldExpiryDate.Text = DateTime.Now.ToString();
            FillComboBox();
            fridge = new StandardFridge(this);
            fridge.Filler.FillFridge();
        }

        private void btnDeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            string[] Ingredient = lstFridgeContent.SelectedItem.ToString().Split(' ');
            //try
            //{
                AbstractIngredient ingredient = FactoryPicker.Instance.Pick(Ingredient[0]).
                                                Create(Convert.ToDouble(Ingredient[2]), Convert.ToDateTime(Ingredient[4]));
                fridge.DeleteIngredientFromDataBase(fridge.Filler.dataBasePull.DataBase, ingredient);
            //}
            //catch (Exception) { MessageBox.Show("Deleting failed", "Information", 
            //                    MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshPage();
        }
    }
}
