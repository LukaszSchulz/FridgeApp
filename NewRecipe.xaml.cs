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
    public partial class NewRecipe : Window //okno z funkcjami dodawania nowych przepisów
    {
        private List<AbstractIngredient> list = new List<AbstractIngredient>();//robocza lista składników, 
                                                                                //na podstawie której powstanie lista przepisu
        public OnlineDataBase DataBase { get; }//zapewnia dostęp do bazy danych

        public NewRecipe(OnlineDataBase dataBase)
        {
            InitializeComponent();
            FillComboBox();//wypełnia comboBox przy inicjalizacji
            DataBase = dataBase;
        }

        private void FillComboBox()//metoda wypełniająca combobox wszystkimi zaprogramowanymi nazwami składników
        {
            foreach (AbstractIngredientFactory IF in FactoryPicker.Instance.listOfFactories)
            {
                cmbIngredientList.Items.Add(IF.Name);
            }
        }

        private void btnAddIngredientToRecipe_Click(object sender, RoutedEventArgs e)//dodaje nowy składnik na listę roboczą 'list'
        {
            lstIngredients.Items.Clear();//czyści listę składników, przed wypełnieniem jej jej aktualną wersją

            AbstractIngredientFactory factory = FactoryPicker.Instance.Pick(cmbIngredientList.SelectedItem.ToString());
                                                    //wybiera odpowiednią fabrykę na podstawie wybranej nazwy
            AbstractIngredient ingredient = factory.Create(Convert.ToDouble(txtAmount.Text));
                                                    //tworzy nowy składnik z parametrami podanymi w formularzu przez użytkownika
            list.Add(ingredient);//dodaje nowy składnik na listę roboczą
            FillTheList();//wypełnia listę składników na liście roboczej na nowo
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e) //Usuwa z listy roboczej 'list' składnik, który nie ma się 
        {                                                               //znaleźć w ostatecznej wersji przepisu
            foreach(AbstractIngredient AI in list) //Porównuje parametry składnika z ich opisem na listboxie i kiedy znajdzie 
                                                    //identyczne - usuwa je i aktualizuje widok
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

        private void FillTheList()//Wypełnia listview składnikami z listy roboczej 'list'
        {
            foreach (AbstractIngredient AI in list)
            {
                lstIngredients.Items.Add($"{AI.Name} {AI.Amount} {AI.Unit}"); //dodaje do listviewboxa string 
                                                                                //z parametrami składnika
            }
        }

        private void cmbIngredientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
                                                                //przy wskazaniu rodzaju składnika, pojawia się nazwa jednostki,
                                                                //w której użytkownik powinien podać ilość składnika
        {
            lblUnit.Content = FactoryPicker.Instance.Pick(cmbIngredientList.SelectedItem.ToString()).Create(0).Unit;
                                                                //factorypicker tworzy instancję pustego składnika wybranego rodzaju,
                                                                //żeby uzyskać dostęp do nazwy jego jednostki
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)//metoda zatwierdzająca nowy przepis, tworząca jego instancję
        {                                                           //oraz dodająca ją do bazy danych
            if(txtName.Text!=null && txtDescription.Text != null && list.Count > 0) //metoda działa wyłącznie, jeśli wszystkie pola
            {                                                                       //zostaną wypełnione
                try
                {
                    DataBase.AddRecipeToDatabase(new StandardRecipeFactory().   //metoda wykorzystuje fabrykę, do stworzenia nowej
                        CreateRecipe(txtName.Text, list, txtDescription.Text)); //instancji przepisu
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    new NewRecipe(DataBase).Show();//po zapisaniu, otwiera się nowe okno nowego przepisu
                    this.Close();                   //a to zostaje zamknięte
                }
            }
            else
            {
                MessageBox.Show("Please fill the form first!", "Information", //w przypadku, kiedy wszystkie pola nie są wypełnione, 
                    MessageBoxButton.OK, MessageBoxImage.Information);       //wyświetla się informacja z prośbą o ich wypełnienie
            }
        }
    }
}
