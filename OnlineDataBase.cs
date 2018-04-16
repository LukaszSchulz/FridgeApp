using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace FridgeWPF
{
    public abstract class OnlineDataBase
    {
        public RecipeConverter Converter { get; protected set; } = new RecipeConverter();
        public abstract string ConnectionString { get; protected set; }
        public MySqlConnection MYSQLConnection { get; set; }
        public MySqlCommand MYSQLCommand { get; set; }
        public SqlConnection SQLConnection { get; set; }
        public SqlCommand SQLCommand { get; set; }

        public string ServerName { get; protected set; }
        public string DataBaseName { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }

        public abstract void AddRecipeToDatabase(AbstractRecipe recipe);

        public abstract void AddIngredientToDatabase(AbstractIngredient ingredient);

        public abstract void DeleteIngredientFromDatabase(AbstractIngredient ingredient);

        public void SetDatabase(string server, string dataBase, string username, string password)
        {
            ServerName = server;
            DataBaseName = dataBase;
            Password = password;
            Username = username;

            ConnectionString = $"Server={ServerName}; " +
                                    $"Database={DataBaseName}; " +
                                    $"Uid={Username}; " +
                                    $"Pwd={Password};";
        }
    }
}