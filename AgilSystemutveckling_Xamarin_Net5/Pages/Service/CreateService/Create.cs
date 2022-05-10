using AgilSystemutveckling_Xamarin_Net5.TestModels;
using Dapper;
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
            
            MySqlConnection connection = new MySqlConnection(connString);

            var cmdText = @$"INSERT INTO Users (Username, Password, Address, AccessId)
                                VALUES (@Username, @Password, @Address, @AccessId)";


            var cmd = new MySqlCommand(cmdText, connection);
            // cmd.Parameters.AddWithValue($"@Id", author.Id); - same as above comment.
            //cmd.Parameters.AddWithValue($"@FullNameId", user.FullNameId);
            cmd.Parameters.AddWithValue($"@Username", user.Username);
            cmd.Parameters.AddWithValue($"@Password", user.Password);
            cmd.Parameters.AddWithValue($"@Address", user.Address);
           // cmd.Parameters.AddWithValue($"@Blocked", user.Blocked);
            //cmd.Parameters.AddWithValue($"@AccessId", user.AccessId);
            connection.Open();
            int r = cmd.ExecuteNonQuery();
            connection.Close();

            return user;
        }
        #endregion

        #region Author related
        public static Models.Author AddAuthor(Models.Author author)
        {
            // author.Id = "<input from user>" - if auto_increment cant be used.

            MySqlConnection connection = new MySqlConnection(connString);


            var cmdText = @$"INSERT INTO Authors (AuthorName)
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

        public static Product AddProduct(Product product)
        {

            MySqlConnection connection = new MySqlConnection(connString);

            var cmdText = @$"INSERT INTO Products (Title, AuthorName, CategoryName, SubCategoryName)
                                VALUES (@Title, @AuthorName, @CategoryName, @SubCategoryName)";

            var cmd = new MySqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue($"@Title", product.Title);
            cmd.Parameters.AddWithValue($"@AuthorName", product.AuthorName);
            cmd.Parameters.AddWithValue($"@CategoryName",  product.CategoryName);
            cmd.Parameters.AddWithValue($"@SubCategoryName", product.SubCategoryName);
            connection.Open();
            int r = cmd.ExecuteNonQuery();
            connection.Close();

            return product;
        }

        #region Category related
        public static Models.Category AddCategory(Models.Category category)
        {

            MySqlConnection connection = new MySqlConnection(connString);

            var cmdText = @$"INSERT INTO Categories (CategoryName)
                                VALUES (@CategoryName)";

            var cmd = new MySqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue($"@CategoryName", category.CategoryName);

            connection.Open();
            int r = cmd.ExecuteNonQuery();
            connection.Close();


            return category;
        }
        #endregion

        #region Subcategory related
        public static Models.SubCategory AddSubCategory(Models.SubCategory subcategory)
        {

            MySqlConnection connection = new MySqlConnection(connString);

            var cmdText = @$"INSERT INTO SubCategory (SubCategoryName)
                                VALUES (@SubCategoryName)";

            var cmd = new MySqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue($"@SubCategoryName", subcategory.SubCategoryName);

            connection.Open();
            int r = cmd.ExecuteNonQuery();
            connection.Close();

            return subcategory;
        }
        #endregion

    }
}
