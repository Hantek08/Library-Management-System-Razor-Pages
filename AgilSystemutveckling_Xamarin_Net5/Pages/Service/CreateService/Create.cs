using AgilSystemutveckling_Xamarin_Net5.Models;
using AgilSystemutveckling_Xamarin_Net5.Constants;
using Dapper;
using MySqlConnector;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.Service.CreateService
{
    public class Create
    { 
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321;";

        #region User related
        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static void AddUser(Users user)
        {
            FirstNames firstName = new();
            LastNames lastName = new();

            // Tried to use constant connectionstring here
            MySqlConnection connection = new MySqlConnection(Constant.connectionString);

            // WORKS
            var sql1 = @"INSERT INTO FirstNames (FirstName)
                                VALUES (@FirstName);
                               INSERT INTO LastNames (LastName)
                                VALUES (@LastName);";

            var cmdFirstLastNames = new MySqlCommand(sql1, connection);

            cmdFirstLastNames.Parameters.AddWithValue($"@FirstName", firstName.FirstName);
            cmdFirstLastNames.Parameters.AddWithValue($"@LastName", lastName.LastName);

            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try { int r = cmdFirstLastNames.ExecuteNonQuery(); }
                catch (Exception ex) { /* throw exception*/ }
                connection.Close();
            }
            else { /* connection is not open */ }

            FullNames fullName = new();

            var sql2 = @$"INSERT INTO FullNames (FirstName.Id, LastName.Id) 
                                VALUES (@FirstName.Id, LastName.Id)";

            var cmdFullNameAdd = new MySqlCommand(sql2, connection);

            cmdFirstLastNames.Parameters.AddWithValue($"@FirstName.Id", fullName.FirstNameId);
            cmdFirstLastNames.Parameters.AddWithValue($"@LastName.Id", fullName.LastNameId);

            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try { int r = cmdFullNameAdd.ExecuteNonQuery(); }
                catch (Exception ex) { /* throw exception */ }

                connection.Close();
            }
            else { /* connection is not open */ }

            var sql3 = $@"INSERT INTO Users (Username, Password, Address, AccessId)
                                    VALUES(@Username, @Password, @Address, @AccessId)";

            var cmdUser = new MySqlCommand(sql3, connection);

            // error handling around inputs (remove if unnecessary)
            if (user.Username != null && user.Username != "admin")
                cmdUser.Parameters.AddWithValue($"@Username", user.Username);
            if (user.Password != null && user.Password.Length > 7)
                cmdUser.Parameters.AddWithValue($"@Password", user.Password);
            if (user.Address != null)
                cmdUser.Parameters.AddWithValue($"@Address", user.Address);
            if (user.AccessId > -1 && user.AccessId < 5)
                cmdUser.Parameters.AddWithValue($"@AccessId", user.AccessId);

            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try { int r = cmdUser.ExecuteNonQuery(); }
                catch (Exception ex) { /* throw exception*/ }
                connection.Close();
            }
            else { /* connection is not open */ }

            // Update user with fullnameid

            var sql4 = $@"UPDATE Users
                                SET Users.FullNameId = (
	                            SELECT FullNames.Id
	                            FROM FullNames
                                WHERE FullNames.Id = Users.Id);";

            var updateWithFullName = new MySqlCommand(sql4, connection);

        }

        /// <summary>
        /// Adds a user asynchronously to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<Users> AddUserAsync(Users user)
        {
            FirstNames firstName = new();
            LastNames lastName = new();
            MySqlConnection connection = new MySqlConnection(connString);

            // WORKS
            var sql1 = @"INSERT INTO FirstNames (FirstName)
                                VALUES (@FirstName);
                               INSERT INTO LastNames (LastName)
                                VALUES (@LastName);";

            var cmdFirstLastNames = new MySqlCommand(sql1, connection);

            cmdFirstLastNames.Parameters.AddWithValue($"@FirstName", firstName.FirstName);
            cmdFirstLastNames.Parameters.AddWithValue($"@LastName", lastName.LastName);

            await connection.OpenAsync();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try { int r = await cmdFirstLastNames.ExecuteNonQueryAsync(); }
                catch (Exception ex) { /* throw exception*/ }
                await connection.CloseAsync();
            }
            else { /* connection is not open */ }

            FullNames fullName = new();

            var sql2 = @$"INSERT INTO FullNames (FirstName.Id, LastName.Id) 
                                VALUES (@FirstName.Id, LastName.Id)";

            var cmdFullNameAdd = new MySqlCommand(sql2, connection);

            cmdFirstLastNames.Parameters.AddWithValue($"@FirstName.Id", fullName.FirstNameId);
            cmdFirstLastNames.Parameters.AddWithValue($"@LastName.Id", fullName.LastNameId);

            await connection.OpenAsync();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try { int r = await cmdFullNameAdd.ExecuteNonQueryAsync(); }
                catch (Exception ex) { /* throw exception */ }

                await connection.CloseAsync();
            }
            else { /* connection is not open */ }

            var sql3 = $@"INSERT INTO Users (Username, Password, Address, AccessId)
                                    VALUES(@Username, @Password, @Address, @AccessId)";

            var cmdUser = new MySqlCommand(sql3, connection);

            if (user.Username != null && user.Username != "admin")
                cmdUser.Parameters.AddWithValue($"@Username", user.Username);
            if (user.Password != null && user.Password.Length > 7)
                cmdUser.Parameters.AddWithValue($"@Password", user.Password);
            if (user.Address != null)
                cmdUser.Parameters.AddWithValue($"@Address", user.Address);
            if (user.AccessId > -1 && user.AccessId < 5)
                cmdUser.Parameters.AddWithValue($"@AccessId", user.AccessId);

            await connection.OpenAsync();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try { int r = await cmdUser.ExecuteNonQueryAsync(); }
                catch (Exception ex) { /* throw exception*/ }
                await connection.CloseAsync();
            }
            else { /* connection is not open */ }

            // Update user with fullnameid

            var sql4 = $@"UPDATE Users
                                SET Users.FullNameId = (
	                            SELECT FullNames.Id
	                            FROM FullNames
                                WHERE FullNames.Id = Users.Id);";

            var updateWithFullName = new MySqlCommand(sql4, connection);

            return user;
        }
        #endregion

        #region Author related
        /// <summary>
        /// Adds an author to the database.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public static void AddAuthor(Authors author)
        {

            var sql = @$"INSERT INTO Authors (AuthorName)
                                VALUES (@{author.AuthorName})";

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                if(connection.State == System.Data.ConnectionState.Open)
                    connection.Execute(sql);
            }
        }

        /// <summary>
        /// Adds an author asynchronously to the database.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public static async Task<Authors> AddAuthorAsync(Authors author)
        {

            var sql = @$"INSERT INTO Authors (AuthorName)
                                VALUES (@{author.AuthorName})";

            using (var connection = new MySqlConnection(connString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql);
                await connection.CloseAsync();
            }

            return author;
        }
        #endregion

        #region Product related
        public static void AddProduct(Products product)
        {
            static List<Categories> GetAllCategories()
            {
                var sql = @$"SELECT Id, CategoryName 
                                    FROM Categories";
                var categories = new List<Categories>();
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                        categories = connection.Query<Categories>(sql).ToList();
                }

                return categories;
            }

            static List<SubCategories> GetAllSubCategories()
            {
                var sql = @$"SELECT Id, SubCategoryName 
                                    FROM SubCategories";
                var subCategories = new List<SubCategories>();
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                        subCategories = connection.Query<SubCategories>(sql).ToList();
                }

                return subCategories;
            }

            int AuthorId = 0;
            int CategoryId = 0;
            int SubCategoryId = 0;

            bool AuthorExists = false;
            bool CategoryExists = false;
            bool SubCategoryExists = false;

            List<Authors> authors = GetService.Get.GetAllAuthors();
            foreach (var author in authors)
            {
                if (author.AuthorName == product.AuthorName)
                {
                    AuthorId = author.Id;
                    AuthorExists = true;
                    break;
                }
            }

            if (AuthorExists == false)
            {
                var sql = @$"INSERT INTO Authors (AuthorName) 
                                    VALUES ('{product.AuthorName}')";
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Execute(sql);
                }

                var sql2 = @$"SELECT Id
                                    FROM Authors
                                    WHERE AuthorName = '{product.AuthorName}'";

                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                        AuthorId = connection.QuerySingle<int>(sql2);
                }
            }

            List<Categories> categories = GetAllCategories();
            foreach (var category in categories)
            {
                if (category.CategoryName == product.CategoryName)
                {
                    CategoryId = category.Id;
                    CategoryExists = true;
                    break;
                }
            }

            if (CategoryExists == false)
            {
                var sql = @$"INSERT INTO Categories (CategoryName) 
                                    VALUES ('{product.CategoryName}')";
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Execute(sql);
                }

                var sql2 = @$"SELECT Id
                                    FROM Categories
                                    WHERE CategoryName = '{product.CategoryName}'";

                using (var connection = new MySqlConnection(connString))
                {

                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                        CategoryId = connection.QuerySingle<int>(sql2);
                }
            }

            List<SubCategories> subCategories = GetAllSubCategories();
            foreach (var subCategory in subCategories)
            {
                if (subCategory.SubCategoryName == product.SubCategoryName)
                {
                    SubCategoryId = subCategory.Id;
                    SubCategoryExists = true;
                    break;
                }
            }

            if (SubCategoryExists == false)
            {
                var sql = @$"insert into SubCategories (SubCategoryName) 
                        values ('{product.SubCategoryName}')";
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Execute(sql);
                }

                var sql2 = @$"SELECT Id
                                FROM SubCategories
                                where SubCategoryName = '{product.SubCategoryName}'";

                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                        SubCategoryId = connection.QuerySingle<int>(sql2);
                }
            }

            var sqlMain = @$"INSERT INTO Products (Title, Description, AuthorId, CategoryId,
                                    SubCategoryId, UnitsInStock, ImgUrl) 
                                    VALUES ('{product.Title}', '{product.Description}', {AuthorId}, {CategoryId}, 
                                    {SubCategoryId}, {product.UnitsInStock}, '{product.ImgUrl}')";
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Execute(sqlMain);
            }
        }

        #endregion

        #region Category related
        /// <summary>
        /// Adds new category to the database.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static void AddCategory(Categories category)
        {
            var cmdText = @$"INSERT INTO Categories (CategoryName)
                                VALUES (@CategoryName)";

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Execute(cmdText);
            }
        }

        /// <summary>
        /// Adds new category asynchronously to the database.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static async Task<Categories> AddCategoryAsync(Categories category)
        {
            var cmdText = @$"INSERT INTO Categories (CategoryName)
                                VALUES (@CategoryName)";

            using (var connection = new MySqlConnection(connString))
            {
                await connection.OpenAsync();
                if (connection.State == System.Data.ConnectionState.Open)
                    await connection.ExecuteAsync(cmdText);
            }

            return category;
        }
        #endregion

        #region Subcategory related
        /// <summary>
        /// Adds new subcategory to the database.
        /// </summary>
        /// <param name="subcategory"></param>
        /// <returns></returns>
        public static void AddSubCategory(SubCategories subcategory)
        {

            var cmdText = @$"INSERT INTO SubCategory (SubCategoryName)
                                VALUES (@SubCategoryName)";

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Execute(cmdText);
            }
        }

        /// <summary>
        /// Adds new subcategory asynchronously to the database.
        /// </summary>
        /// <param name="subcategory"></param>
        /// <returns></returns>
        public static async Task<SubCategories> AddSubCategoryAsync(SubCategories subcategory)
        {

            var cmdText = @$"INSERT INTO SubCategory (SubCategoryName)
                                VALUES (@SubCategoryName)";

            using (var connection = new MySqlConnection(connString))
            {
                await connection.OpenAsync();
                if (connection.State == System.Data.ConnectionState.Open)
                    await connection.ExecuteAsync(cmdText);
            }
            return subcategory;
        }
        #endregion



    }
}
