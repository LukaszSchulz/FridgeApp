using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace FridgeWPF
{
    public abstract class OnlineDataBase
    {
        public abstract string ConnectionString { get; }
        public MySqlConnection Connection { get; set; }
        public MySqlCommand Command { get; set; }

        public abstract void AddRecipeToDatabase(AbstractRecipe recipe);

        public abstract void AddIngredientToDatabase(AbstractIngredient ingredient);

        public abstract void DeleteIngredientFromDatabase(AbstractIngredient ingredient);
    }
}