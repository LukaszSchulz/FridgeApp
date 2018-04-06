using FridgeWPF;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FridgeWPF
{
    public class DataBasePull
    {
        DataTable dataTable = new DataTable();
        public OnlineDataBase DataBase;
        string CommandString;
        MySqlDataReader dataReader;
        AbstractFridge fridge;
       // IngredientFactory ingredientFactory;

        public DataBasePull(OnlineDataBase dataBase, string commandString, AbstractFridge fridge)
        {
            DataBase = dataBase;
            CommandString = commandString;
            this.fridge = fridge;
        }

        public void PullDataFromDataBase()
        {
            DataBase.Connection = new MySqlConnection(DataBase.ConnectionString);
            DataBase.Command = new MySqlCommand(CommandString, DataBase.Connection);

            using (DataBase.Connection)
            {
                using (DataBase.Command)
                {
                    try{DataBase.Connection.Open();}
                    catch (InvalidOperationException ex) { MessageBox.Show(ex.Message); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                    dataReader = DataBase.Command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        try
                        {
                            {
                                fridge.AddIngredient(FactoryPicker.Instance.Pick(dataReader.GetString(0)).Create(dataReader.GetDouble(1), dataReader.GetDateTime(2)));
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }
                }

                DataBase.Connection.Close();
            }
        }
    }
}
