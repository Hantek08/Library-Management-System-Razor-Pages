using AgilSystemutveckling_Xamarin_Net5.TestModels;
using MySqlConnector;
using AgilSystemutveckling_Xamarin_Net5.Constants;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.testService.CreateService
{
    public class Create
    {

        #region User related
        public static User AddUser(User user)
        {
            MySqlConnection connection = new MySqlConnection(Constant.connectionString);

            var cmdText = @$"INSERT INTO User (Username, Password, FirstName, LastName Address, AccessId, Blocked) 
                                VALUES (@Username, @Password, @FirstName, @LastName @Address, @AccessId, @Blocked)";


            var cmd = new MySqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue($"@Username", user.Username);
            cmd.Parameters.AddWithValue($"@Password", user.Password);
            cmd.Parameters.AddWithValue($"@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue($"@LastName", user.LastName);
            cmd.Parameters.AddWithValue($"@Address", user.Address);
            cmd.Parameters.AddWithValue($"@AccessId", user.AccessId);
            cmd.Parameters.AddWithValue($"@Blocked", user.Blocked);
            connection.Open();
            int r = cmd.ExecuteNonQuery();
            connection.Close();

            return user;
        }
        #endregion

        #region Author related
        public static Author AddAuthor(Author author)
        {
            // author.Id = "<input from user>" - if auto_increment cant be used.
            
            MySqlConnection connection = new MySqlConnection(Constant.connectionString);


            var cmdText = @$"INSERT INTO Author (AuthorName)
                                VALUES (@AuthorName)";


            var cmd = new MySqlCommand(cmdText, connection);
            // cmd.Parameters.AddWithValue($"@Id", author.Id); - same as above comment.
            cmd.Parameters.AddWithValue($"@AuthorName", author.AuthorName);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return author;
        }
        #endregion

        #region Category related
        public static Category AddCategory(Category category)
        {
            MySqlConnection connection = new MySqlConnection(Constant.connectionString);


            var cmdText = @$"INSERT INTO Category (CategoryName)
                                VALUES (@CategoryName)";


            var cmd = new MySqlCommand(cmdText, connection);
            
            cmd.Parameters.AddWithValue($"@Category", category.CategoryName);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return category;
        }

        #endregion
    }
}
