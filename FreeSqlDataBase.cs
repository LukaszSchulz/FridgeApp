using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace FridgeWPF
{
    public class FreeSqlDataBase : OnlineDataBase
    {
        public override string ConnectionString { get; protected set; } = @"Server=sql11.freesqldatabase.com;
                                                            Database=sql11227333;
                                                            Uid=sql11227333;
                                                            Pwd=F48xDrZZcw;";
        
        public override void AddIngredientToDatabase(AbstractIngredient ingredient)
        {
            try
            {
                string CommandString = $"INSERT INTO FridgeContent (Name, Amount, ExpiryDate) " +
                                   $"VALUES(@Name, @Amount, @ExpiryDate)";
                using (MYSQLConnection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                {
                    MYSQLConnection.Open();
                    using (MYSQLCommand = new MySql.Data.MySqlClient.MySqlCommand(CommandString, MYSQLConnection))
                    {
                        MYSQLCommand.Parameters.AddWithValue("@Name", ShortName(ingredient.Name));
                        MYSQLCommand.Parameters.AddWithValue("@Amount", ingredient.Amount);
                        MYSQLCommand.Parameters.AddWithValue("@ExpiryDate", ingredient.ExpiryDate);
                        MYSQLCommand.ExecuteNonQuery();
                    }

                    MYSQLConnection.Close();
                }
            }
            catch (InvalidOperationException ex) { MessageBox.Show(ex.Message, "FreeSQL.AddIngredient"); }
            catch (MySqlException ex) { MessageBox.Show(ex.Message, "FreeSQL.AddIngredient"); }
            catch(Exception ex) { MessageBox.Show(ex.Message, "FreeSQL.AddIngredient"); }
        }

        public override void DeleteIngredientFromDatabase(AbstractIngredient ingredient)
        {
            string amountWithaDot = CommaFormat(ingredient.Amount.ToString());
            string amountwithComa = ingredient.Amount.ToString();
            
            string CommandString = $"DELETE FROM FridgeContent " +
                                    $"WHERE Name = '{ShortName(ingredient.Name)}' " +
                                    $"AND Amount = '{amountWithaDot}' " +
                                    $"AND ExpiryDate = '{ingredient.ExpiryDate}' " +
                                    $"LIMIT 1";

            using(MYSQLConnection = new MySqlConnection(ConnectionString))
            {
                MYSQLConnection.Open();
                MYSQLCommand = new MySqlCommand(CommandString, MYSQLConnection);
                MYSQLCommand.ExecuteNonQuery();
                MYSQLConnection.Close();
            }
        }

        string ShortName(string longName)
        {
            string shortName;

            string[] dividedName = longName.Split('.');

            shortName = dividedName[(dividedName.Length - 1)];

            return shortName;
        }

        private string CommaFormat(string input) //zmienia kropkę na przecinek, żeby ujednolicić format doubla z bazy i VS.
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ',')
                {
                    output += '.';
                }
                else
                {
                    output += input[i];
                }
            }
            return output;
        }

        public List<AbstractIngredient> ConvertToRecipe(string recipeAsString)
        {
            Converter = new RecipeConverter();
            return Converter.FromStringToListOfIngredients(recipeAsString);
        }

        public string ConvertToString(AbstractRecipe recipe) // zwraca listę składników w postaci stringa
        {
            string output="";

            Converter = new RecipeConverter();

            for(int i = 0; i< recipe.ListOfIngredients.Count; i++)
            {
                output += Converter.FromIngredientToString(recipe.ListOfIngredients[i]);
                if (i < recipe.ListOfIngredients.Count - 1)
                {
                    output += ";";
                }
            }
            return output;
        }

        public override void AddRecipeToDatabase(AbstractRecipe recipe) //zapisuje nowy przepis w bazie danych
        {
            try
            {
                string CommandString = $"INSERT INTO Recipes (Name, Ingredients, Description) " +
                                   $"VALUES(@Name, @Ingredients, @Description)";
                using (MYSQLConnection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                {
                    MYSQLConnection.Open();
                    using (MYSQLCommand = new MySql.Data.MySqlClient.MySqlCommand(CommandString, MYSQLConnection))
                    {
                        MYSQLCommand.Parameters.AddWithValue("@Name", ShortName(recipe.Name));
                        MYSQLCommand.Parameters.AddWithValue("@Ingredients", ConvertToString(recipe));
                        MYSQLCommand.Parameters.AddWithValue("@Description", recipe.Description);
                        MYSQLCommand.ExecuteNonQuery();
                    }

                    MYSQLConnection.Close();
                }
            }
            catch (InvalidOperationException ex) { MessageBox.Show(ex.Message, "FreeSQL.AddRecipe"); }
            catch (MySqlException) { MessageBox.Show($"Adding the new recipe failed. \n" +
                $"Maybe you already have recipe called {recipe.Name}?", "FreeSQL.AddRecipe"); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "FreeSQL.AddRecipe"); }
        }
    }
}