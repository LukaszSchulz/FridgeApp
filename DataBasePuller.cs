using FridgeWPF;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FridgeWPF
{
    public class DataBasePuller //służy pobieraniu danych z bazy i pakowaniu ich w formie odpowiednich klas do lodówki
    {
        public OnlineDataBase DataBase;
        MySqlDataReader dataReader;

        public DataBasePuller(OnlineDataBase dataBase )
        {
            DataBase = dataBase;
        }

        public void PullIngredientsFromDataBase(string commandString, AbstractFridge fridge)
        {
            DataBase.MYSQLConnection = new MySqlConnection(DataBase.ConnectionString);
            DataBase.MYSQLCommand = new MySqlCommand(commandString, DataBase.MYSQLConnection);

            using (DataBase.MYSQLConnection)
            {
                using (DataBase.MYSQLCommand)
                {
                    try
                    {
                        DataBase.MYSQLConnection.Open();
                        {
                            dataReader = DataBase.MYSQLCommand.ExecuteReader();
                            while (dataReader.Read())
                            {
                                fridge.AddIngredient
                                (FactoryPicker.Instance.Pick(dataReader.GetString(0))
                                .Create(Convert.ToDouble(CommaFormat(dataReader.GetString(1))), dataReader.GetDateTime(2)));
                            }
                        }
                    }
                    catch (Exception)
                    { MessageBox.Show("There has been a problem with connecting to the Database", "DBPuller.PullIngredients"); }
                }
                DataBase.MYSQLConnection.Close();
            }
        }

        private string CommaFormat(string input) //zmienia kropkę na przecinek, żeby ujednolicić format doubla z bazy i VS.
        {
                string output="";
            for(int i = 0; i<input.Length; i++)
            {
                if (input[i] == '.')
                {
                    output += ',';
                }
                else
                {
                    output += input[i];
                }
            }
            return output;
        }


        public void PullRecipesFromDatabase(string commandString, IRecipeBook recipeBook)
        {
            AbstractRecipeFactory recipeFactory = new StandardRecipeFactory();
            DataBase.MYSQLConnection = new MySqlConnection(DataBase.ConnectionString);
            DataBase.MYSQLCommand = new MySqlCommand(commandString, DataBase.MYSQLConnection);

            using (DataBase.MYSQLConnection)
            {
                using (DataBase.MYSQLCommand)
                {
                    try
                    {
                        DataBase.MYSQLConnection.Open();
                        dataReader = DataBase.MYSQLCommand.ExecuteReader();
                    
                        while (dataReader.Read())
                        {
                            {
                                recipeBook.Recipebook.Add(recipeFactory.CreateRecipe(dataReader.GetString(0),
                                    DataBase.Converter.FromStringToListOfIngredients(dataReader.GetString(1)),
                                    dataReader.GetString(2)));
                            }
                        }
                        
                    }
                    catch (MySqlException) { MessageBox.Show("Failed to connect with the database"); }
                    catch (InvalidOperationException ex) { MessageBox.Show(ex.Message); }
                }
                DataBase.MYSQLConnection.Close();
            }
        }
    }
}
