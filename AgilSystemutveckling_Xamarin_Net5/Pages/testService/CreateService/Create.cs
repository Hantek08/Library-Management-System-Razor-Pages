using AgilSystemutveckling_Xamarin_Net5.TestModels;
using MySqlConnector;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.testService.CreateService
{
    public class Create
    {
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321";
        #region Author related
        public static TestModels.Author AddAuthor(Author author)
        { 
            // Placeholder code, should be replaced with user input when adding a new author.
            MySqlConnection connection = new MySqlConnection(connString);


            var cmdText = @$"INSERT INTO Author 
                                VALUES (@AuthorName)";


            using var cmd = new MySqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue($"@AuthorName", author.AuthorName);
            connection.Open();

            connection.Close();
            return author;
        }
        #endregion
    }
}
