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
    /// Interaction logic for NewRecipe.xaml
    /// </summary>
    public partial class NewRecipe : Window
    {
        private List<AbstractIngredient> list = new List<AbstractIngredient>();

        public OnlineDataBase DataBase { get; }

        public NewRecipe(OnlineDataBase dataBase)
        {
            InitializeComponent();
            FillComboBox();
            DataBase = dataBase;
        }

        private void FillComboBox()
        {
            foreach (AbstractIngredientFactory IF in FactoryPicker.Instance.listOfFactories)
            {
                cmbIngredientList.Items.Add(IF.Name);
            }
        }

        private void btnAddIngredientToRecipe_Click(object sender, RoutedEventArgs e)
        {
            lstIngredients.Items.Clear();

            AbstractIngredientFactory factory = FactoryPicker.Instance.Pick(cmbIngredientList.SelectedItem.ToString());
            AbstractIngredient ingredient = factory.Create(Convert.ToDouble(txtAmount.Text));

            list.Add(ingredient);
            FillTheList();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            foreach(AbstractIngredient AI in list)
            {
                if(lstIngredients.SelectedItem.ToString().Split(' ')[0] == AI.Name &&
                    lstIngredients.SelectedItem.ToString().Split(' ')[1] == AI.Amount.ToString())
                {
                    list.Remove(AI);
                    lstIngredients.Items.Clear();
                    FillTheList();
                    return;
                }
            }
        }

        private void FillTheList()
        {
            foreach (AbstractIngredient AI in list)
            {
                lstIngredients.Items.Add($"{AI.Name} {AI.Amount} {AI.Unit}");
            }
        }

        private void cmbIngredientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblUnit.Content = FactoryPicker.Instance.Pick(cmbIngredientList.SelectedItem.ToString()).Create(0).Unit;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text!=null && txtDescription.Text != null && list.Count > 0)
            {
                try
                {
                    DataBase.AddRecipeToDatabase(new StandardRecipeFactory().
                        CreateRecipe(txtName.Text, list, txtDescription.Text));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("New recipe added successfully!");
                    new NewRecipe(DataBase).Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please fill the form first!", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
