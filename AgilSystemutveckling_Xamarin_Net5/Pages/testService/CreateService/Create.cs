using AgilSystemutveckling_Xamarin_Net5.TestModels;
using MySqlConnector;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.testService.CreateService
{
    public class Create
    {
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321";

        #region User related
        // Needs editing.
        public static User AddUser(User user)
        {
            user.FullNameId = 0;
            user.Username = "<read from input>";
            user.Password = "<read from input>";
            user.Address = "<read from input>";
            user.AccessId = 0;
            user.Blocked = false;

            MySqlConnection connection = new MySqlConnection(connString);

            var cmdText = @$"INSERT INTO Author 
                                VALUES (@FullNameId, @Username, @Password, @Address, @AccessId, @Blocked)";


            var cmd = new MySqlCommand(cmdText, connection);
            // cmd.Parameters.AddWithValue($"@Id", author.Id); - same as above comment.
            cmd.Parameters.AddWithValue($"@FullNameId", user.FullNameId);
            cmd.Parameters.AddWithValue($"@Username", user.Username);
            cmd.Parameters.AddWithValue($"@Password", user.Password);
            cmd.Parameters.AddWithValue($"@Address", user.Address);
            cmd.Parameters.AddWithValue($"@Blocked", user.Blocked);
            connection.Open();
            int r = cmd.ExecuteNonQuery();

            return user;
        }
        #endregion

        #region Author related
        public static Author AddAuthor(Author author)
        {
            // author.Id = "<input from user>" - if auto_increment cant be used.
            
            MySqlConnection connection = new MySqlConnection(connString);


            var cmdText = @$"INSERT INTO Author (AuthorName)
                                VALUES (@AuthorName)";


            var cmd = new MySqlCommand(cmdText, connection);
            // cmd.Parameters.AddWithValue($"@Id", author.Id); - same as above comment.
            cmd.Parameters.AddWithValue($"@AuthorName", author.AuthorName);
            connection.Open();
            cmd.ExecuteNonQuery();

            return author;
        }
        #endregion
    }
}
