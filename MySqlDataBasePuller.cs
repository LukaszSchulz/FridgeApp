using FridgeWPF;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows;

namespace FridgeWPF
{
    public class MySqlDataBasePuller //służy pobieraniu danych z bazy i zawiera narzędzia do pakowania 
                                        //ich w formie odpowiednich klas do lodówki
    {
        public OnlineDataBase DataBase { get; }//umożliwia dostęp do wybranej bazy danych
        DbDataReader dataReader;//umożliwia odczyt danych z bazy danych

        public MySqlDataBasePuller(OnlineDataBase dataBase)
        {
            DataBase = dataBase;
        }

        public void PullIngredientsFromDataBase(string commandString, AbstractFridge fridge)//metoda pobierająca dane dotyczące 
        {                                                                               //składników w bazie danych i umieszcza
                                                                                        //w odpowiedniej lodówce
            DataBase.Connection = new MySqlConnection(DataBase.ConnectionString);  //umożliwia otwarcie połączenia
            DataBase.Command = new MySqlCommand(commandString, (MySqlConnection)DataBase.Connection); //umożliwia przekazanie polecenia
                                                                                                //do bazy danych w MySql
            using (DataBase.Connection)
            {
                using (DataBase.Command)
                {
                    try
                    {
                        DataBase.Connection.Open(); //otwarcie połączenia
                        {
                            dataReader = DataBase.Command.ExecuteReader(); //umożliwia odczyt danych z bazy
                            while (dataReader.Read())
                            {
                                fridge.AddIngredient        //dodaje do lodówki wszystkie składniki dostępne w bazie danych
                                (FactoryPicker.Instance.Pick(dataReader.GetString(0))//wybiera fabryki na podstawie nazwy składnika
                                .Create(Convert.ToDouble(CommaFormat(dataReader.GetString(1))), dataReader.GetDateTime(2)));
                                                                                   //tworzy składniki o parametrach z bazy danych
                            }
                        }
                    }
                    catch (Exception)
                    { MessageBox.Show("There has been a problem with connecting to the Database", "DBPuller.PullIngredients"); }                
                }
                DataBase.Connection.Close();//zamyka połączenie
            }
        }

        private string CommaFormat(string input) //zmienia kropkę na przecinek, żeby ujednolicić format doubla z bazy i VS.
        {
                string output="";//pole, w którym jest tworzony string wyjściowy
            for(int i = 0; i<input.Length; i++)//metoda iteruje przez wszystkie znaki w stringu
            {
                if (input[i] == '.')//w momencie kiedy znak jest kropką, w stringu wyjściowym zapisywany jest jako przecinek
                {
                    output += ',';
                }
                else
                {
                    output += input[i];//jeżeli znak nie jest kropką, jest bezpośrednio dopisywany do stringa wyjściowego
                }
            }
            return output;
        }


        public void PullRecipesFromDatabase(string commandString, IRecipeBook recipeBook)//metoda służąca pobieraniu danych
                                                                                        //dotyczących przepisów i zapisywaniu
        {                                                                               //ich w książce przepisów
            AbstractRecipeFactory recipeFactory = new StandardRecipeFactory();//metoda korzysta z fabryki przepisów do konstruowania
            DataBase.Connection = new MySqlConnection(DataBase.ConnectionString);//umożliwia nawiązanie połączenia
            DataBase.Command = new MySqlCommand(commandString, (MySqlConnection)DataBase.Connection);//umożliwia przekazanie polecenia

            using (DataBase.Connection)
            {
                using (DataBase.Command)
                {
                    try
                    {
                        DataBase.Connection.Open();//otwiera połączenie
                        dataReader = DataBase.Command.ExecuteReader();//umożliwia odczyt danych z bazy
                    
                        while (dataReader.Read())
                        {
                            {
                                recipeBook.Recipebook.Add(recipeFactory.CreateRecipe//pobiera dane zapisane w bazie w postaci
                                    (dataReader.GetString(0),                       //stringów i zapisuje je jako obiekt klasy
                                    DataBase.Converter.FromStringToListOfIngredients //Recipe
                                    (dataReader.GetString(1)), dataReader.GetString(2)));
                            }
                        }                        
                    }
                    catch (MySqlException) { MessageBox.Show("Failed to connect with the database"); }
                    catch (InvalidOperationException ex) { MessageBox.Show(ex.Message); }                
                }
                DataBase.Connection.Close();//zamyka połączenie
            }
        }
    }
}
