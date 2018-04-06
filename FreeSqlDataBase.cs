namespace FridgeWPF
{
    public class FreeSqlDataBase : OnlineDataBase
    {
        public override string ConnectionString { get; } = @"Server=sql11.freesqldatabase.com;
                                                            Database=sql11227333;
                                                            Uid=sql11227333;
                                                            Pwd=F48xDrZZcw;";

        public override void AddRecipeToDatabase(AbstractRecipe recipe)
        {
            Connection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString);
            string CommandText = @"INSERT INTO";
            Command = new MySql.Data.MySqlClient.MySqlCommand(CommandText, Connection);
        }

        public override void AddIngredientToDatabase(AbstractIngredient ingredient)
        {
            string CommandString = $"INSERT INTO FridgeContent (Name, Amount, ExpiryDate) " +
                                   $"VALUES(@Name, @Amount, @ExpiryDate)";
            using (Connection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
            {
                Connection.Open();
                using(Command = new MySql.Data.MySqlClient.MySqlCommand(CommandString, Connection))
                {
                    Command.Parameters.AddWithValue("@Name", ShortName(ingredient.Name));
                    Command.Parameters.AddWithValue("@Amount", ingredient.Amount);
                    Command.Parameters.AddWithValue("@ExpiryDate", ingredient.ExpiryDate);
                    Command.ExecuteNonQuery();
                }
                
                Connection.Close();
            }
        }

        public override void DeleteIngredientFromDatabase(AbstractIngredient ingredient)
        {
            string CommandString = $"DELETE FROM FridgeContent" +
                                    $"WHERE Name = '{ingredient.Name}' " +
                                    $"AND Amount = '{ingredient.Amount}' " +
                                    $"AND ExpiryDate = '{ingredient.ExpiryDate}'";

            using(Connection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
            {
                Command = new MySql.Data.MySqlClient.MySqlCommand(CommandString, Connection);
                Command.ExecuteNonQuery();
            }
        }

        string ShortName(string longName)
        {
            string shortName;

            string[] dividedName = longName.Split('.');

            shortName = dividedName[(dividedName.Length - 1)];

            return shortName;
        }

        
    }
}