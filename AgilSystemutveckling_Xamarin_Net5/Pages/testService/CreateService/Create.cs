using AgilSystemutveckling_Xamarin_Net5.TestModels;
using MySqlConnector;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.testService.CreateService
{
    public class Create
    {
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321";
        #region Author related
        public static TestModels.Author AddAuthor()
        {
            int id = 0;
            string authorName = "";
            var author = new Author { AuthorName = authorName };
            MySqlConnection connection = new MySqlConnection(connString);
            connection.Open();

            var sql = @$"INSERT INTO Author (AuthorName)
                            VALUES {authorName}";

            return author;
        }
        #endregion
    }
}
