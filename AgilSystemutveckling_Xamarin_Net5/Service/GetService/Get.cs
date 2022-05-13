using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using MySqlConnector;
using AgilSystemutveckling_Xamarin_Net5.Models;
using AgilSystemutveckling_Xamarin_Net5.Constants;

namespace AgilSystemutveckling_Xamarin_Net5.Service.GetService
{
    public class Get
    {

        #region Category related
        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        public static List<Categories> GetAllCategories()
        {
            var sql = @$"SELECT Id, CategoryName 
                                FROM Categories";

            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                List<Categories> categories = new List<Categories>();
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    categories = connection.Query<Categories>(sql).ToList();
                    return categories;
                }
                return categories;
            }
        }

        /// <summary>
        /// Get all categories asynchronously.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Categories>?> GetAllCategoriesAsync()
        {
            var sql = @$"SELECT Id, CategoryName 
                                FROM Categories";
            
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                await connection.OpenAsync();
                if (connection.State == System.Data.ConnectionState.Open) 
                {
                    var results = await connection.QueryAsync<Categories>(sql);
                    return results.ToList();
                }
                return null;
            }
        }

        public static List<SubCategories> GetAllSubCategories()
        {
            var sql = @$"SELECT Id, CategoryName 
                                FROM Categories";
            var subCategories = new List<SubCategories>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                subCategories = connection.Query<SubCategories>(sql).ToList();
            }

            return subCategories;
        }

        #endregion


        #region Product related methods

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetAllProducts()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id";
            var name = new List<Products>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
        }

        /// <summary>
        /// Gets all products async.
        /// </summary>
        /// <returns></returns>
        public static  async Task<List<Products>> GetAllProductsAsync()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id";

            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                await connection.OpenAsync();
                var products = connection.QueryAsync<Products>(sql).Result;
                await connection.CloseAsync();
                return products.ToList();
            }
        }

        /// <summary>
        /// Gets specific product by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Products GetProductById(int id)
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            where Products.Id = {id}";
            var products = new Products();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                products = connection.QuerySingle<Products>(sql);
                connection.Close();
            }

            return products;
        }

        /// <summary>
        /// Gets specific product by ID async.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Products> GetProductByIdAsync(int id)
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            where Products.Id = {id}";

            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                await connection.OpenAsync();
                if(connection.State == System.Data.ConnectionState.Open) 
                {
                    var product = await connection.QuerySingleAsync<Products>(sql);
                    await connection.CloseAsync();
                    return product;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets all products sorted by category A-Ö.
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetAllProductsSortedByCategoryAsc()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            ORDER BY Products.Category.Id";

            var products = new List<Products>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                if(connection.State == System.Data.ConnectionState.Open) 
                {
                    products = connection.Query<Products>(sql).ToList();
                    connection.Close();
                    return products;
                }
            }
            return products;
        }

        /// <summary>
        /// Gets all products sorted by category A-Ö async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products>> GetAllProductsSortedByCategoryAscAsync()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            ORDER BY Products.Category.Id";

            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                await connection.OpenAsync();
                if(connection.State == System.Data.ConnectionState.Open)
                {
                    var products = await connection.QueryAsync<Products>(sql);
                    await connection.CloseAsync();

                    return products.ToList();
                }
                return null;
            }
        }


        /// <summary>
        /// Gets all products sorted by category Ö-A.
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetAllProductsSortedByCategoryDesc()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            ORDER BY Products.Category.Id desc";

            var name = new List<Products>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
        }

        /// <summary>
        /// Gets all products sorted by category Ö-A async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products>> GetAllProductsSortedByCategoryDescAsync()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            ORDER BY Products.Category.Id desc";

            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                await connection.OpenAsync();
                if(connection.State == System.Data.ConnectionState.Open) 
                {
                    var name = await connection.QueryAsync<Products>(sql);
                    await connection.CloseAsync();

                    return name.ToList();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetAllBooks()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
				            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
				            Products.UnitsInStock, Products.InStock, Products.ImgUrl
				            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE CategoryId = '1';";

            var books = new List<Products>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                if(connection.State == System.Data.ConnectionState.Open)
                {
                    books = connection.Query<Products>(sql).ToList();
                    connection.Close();
                    return books;
                }
                return books;
                
            }
        }

        /// <summary>
        /// Gets all books async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products>> GetAllBooksAsync()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
				            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
				            Products.UnitsInStock, Products.InStock, Products.ImgUrl
				            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE CategoryId = '1';";

            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                await connection.OpenAsync();
                if(connection.State == System.Data.ConnectionState.Open)
                {
                    var books = connection.Query<Products>(sql).ToList();
                    await connection.CloseAsync();
                    return books;
                }
                return null;
            }
        }


        /// <summary>
        /// Gets a specific book(?)
        /// </summary>
        /// <returns></returns>
        public static Products GetBook()
        {
            /*string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products
                            WHERE Categories = 'Book'";*/

            string? sql = $@"SELECT Title, AuthorId, SubCategoryId
                              FROM Products
                              WHERE Products.CategoryId = '1';";

            Products book = new Products();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                if(connection.State == System.Data.ConnectionState.Open) 
                {
                    book = connection.QuerySingle<Products>(sql);
                    connection.Close();
                    return book;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets a specific book async.
        /// </summary>
        /// <returns></returns>
        public static async Task<Products> GetBookAsync()
        {
            /*string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products
                            WHERE Categories='Book'";*/

            // had issues running  above sql query and tried this instead

            string? sql = $@"SELECT Title, AuthorId, SubCategoryId
                              FROM Products
                              WHERE Products.CategoryId = '1';";



            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                await connection.OpenAsync();
                Products book = await connection.QuerySingleAsync<Products>(sql);
                await connection.CloseAsync();
                return book;
            }
        }

        /// <summary>
        /// Gets a specific ebook.
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetEbook()
        {
            string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products
                            WHERE Categories='E-Book'";

            var name = new List<Products>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
        }

        /// <summary>
        /// Gets a specific ebook async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products>> GetEbookAsync()
        {
            string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products
                            WHERE Categories='E-Book'";

            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                await connection.OpenAsync();
                var ebooks = await connection.QueryAsync<Products>(sql);
                await connection.CloseAsync();
                return ebooks.ToList();
            }
        }



        /// <summary>
        /// Gets a specific movie.
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetMovie()
        {
            string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products 
                            WHERE Categories='Movie'";

            var name = new List<Products>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
        }

        /// <summary>
        /// Gets a specific movie async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products>> GetMovieAsync()
        {
            string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products 
                            WHERE Categories='Movie'";

            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                await connection.OpenAsync();
                var movies = await connection.QueryAsync<Products>(sql);
                await connection.CloseAsync();
                return movies.ToList();
            }
        }

        /// <summary>
        /// Gets a specific book seminar.
        /// </summary>
        /// <returns></returns>
        public static List<Products> BookSeminar()
        {
            string? sql = @$"SELECT Title
                            FROM Products
                            WHERE Categories='Seminarium'";

            var name = new List<Products>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
        }
        #endregion

        #region User related methods
        /// <summary>
        /// Gets all users from Users table.
        /// </summary>
        /// <returns></returns>
        public static List<Users> GetAllUsers()
        {
            var sql = @$"Select Users.Id, Users.Username, Users.Password, Users.Address, Users.Blocked, 
                        FirstNames.FirstName, LastNames.LastName, Access.Level
                        from Users
                        inner join FullNames on Users.FullNameId = FullNames.Id
                        inner join FirstNames on FullNames.FirstNameId = FirstNames.Id
                        inner join LastNames on FullNames.LastNameId = LastNames.Id
                        inner join Access on Users.AccessId = Access.Id";
            var user = new List<Users>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                user = connection.Query<Users>(sql).ToList();
            }

            return user;
        }
        /// <summary>
        /// Gets all users sorted A-Ö.
        /// </summary>
        /// <returns></returns>
        public static List<Users> GetAllUsersOrderedAlphabetically()
        {
            var sql = @$"Select Username 
                            FROM Users
                            ORDER BY Username";
            var user = new List<Users>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                user = connection.Query<Users>(sql).ToList();
            }

            return user;
        }
        /// <summary>
        /// Gets all users sorted Ö-A.
        /// </summary>
        /// <returns></returns>
        public static List<Users> GetAllUsersReverseOrder()
        {
            var sql = @$"Select Username 
                                FROM Users 
                                ORDER BY Username desc";
            var user = new List<Users>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                user = connection.Query<Users>(sql).ToList();
            }

            return user;
        }
        /// <summary>
        /// Gets all users sorted depending on a specific character.
        /// </summary>
        /// <returns></returns>
        public static List<Users> GetAllUsersStartingWithLetter(char a)
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"SELECT Username 
                               FROM Users 
                               WHERE Username = '{a}%'
                               ORDER BY Username";
            var user = new List<Users>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                user = connection.Query<Users>(sql).ToList();
            }

            return user;
        }
        #endregion

        #region Author related methods
        public static List<Authors> GetAllAuthors()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select Id, AuthorName 
                                From Authors";
            var author = new List<Models.Authors>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Models.Authors>(sql).ToList();
            }

            return author;
        }
        public static List<Authors> GetAllAuthorsStartingWithLetter()
        {
            string letter = "<first letter of search query here>";
            var sql = @$"Select AuthorName 
                            from Author 
                            where AuthorName = '{letter}%' 
                            ORDER BY AuthorName";
            var author = new List<Authors>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Authors>(sql).ToList();
            }

            return author;
        }
        public static List<Authors> GetAllAuthorsOrderedAlphabetically()
        {
            var sql = @$"Select Id, AuthorName 
                             from Authors 
                             ORDER BY AuthorName";
            var author = new List<Authors>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Authors>(sql).ToList();
            }

            return author;
        }

        public static List<Authors> GetAllAuthorsReverseOrder()
        {
            var sql = @$"Select Id, AuthorName 
                            from Authors 
                            ORDER BY AuthorName desc";
            var author = new List<Authors>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Authors>(sql).ToList();
            }

            return author;
        }

        public static List<Authors> GetAllAuthorsOrderedById()
        {
            var sql = @$"Select Id, AuthorName 
                             from Authors
                             ORDER BY Id";
            var author = new List<Authors>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Authors>(sql).ToList();
            }

            return author;
        }

        public static List<Authors> GetAllAuthorsReverseOrderId()
        {
            var sql = @$"Select Id, AuthorName 
                            from Authors
                            ORDER BY Id desc";
            var author = new List<Authors>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Authors>(sql).ToList();
            }

            return author;
        }

        #endregion

        #region Names related

        /// <summary>
        /// Gets all first names from FirstNames table.
        /// </summary>
        /// <returns></returns>
        public static List<FirstNames> GetAllFirstNames()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"SELECT FirstName
                                FROM FirstNames";
            var firstName = new List<FirstNames>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                firstName = connection.Query<FirstNames>(sql).ToList();
            }

            return firstName;
        }
        /// <summary>
        /// Gets all last names from LastNames table.
        /// </summary>
        /// <returns></returns>
        public static List<LastNames> GetAllLastNames()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select LastName
                                From LastNames";
            var lastName = new List<LastNames>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                lastName = connection.Query<LastNames>(sql).ToList();
            }

            return lastName;
        }

        #endregion

        #region Loan related
        public static void UserAction (int userID, int productID,int actionID) 
        {
            MySqlConnection connection = new MySqlConnection(Constant.connectionString);

            var cmdText = @$"INSERT INTO History (UserId, ProductId, DateTime, ActionId)
                                    VALUES (@UserId, @ProductId, @DateTime, @ActionId)";

            var cmd = new MySqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue($"@UserID", userID);
            cmd.Parameters.AddWithValue($"@ProductID", productID);
            cmd.Parameters.AddWithValue($"@DateTime", DateTime.Now);
            cmd.Parameters.AddWithValue($"@ActionID", actionID);

            connection.Open();
            int r = cmd.ExecuteNonQuery();
            connection.Close();
        }
        /// <summary>
        /// Returns number of loans a customer has made.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int NumberOfItemsLentToCustomer(Users user)
        { 
            var sql = $@"SELECT *
                                FROM History
                                WHERE History.UserId = @Users.Id;";

            var products = new List<Products>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                products = connection.Query<Products>(sql).ToList();
            }

            return products.Count();
        }

        #endregion

        #region History related
        public static List<History> GetAllHistories()
        {
            // !! Must be tested when histories are added.

            string? sql = @$"SELECT History.Id, History.UserId, History.ProductId, History.ActionId, History.Datetime
                            FROM History
                            INNER JOIN Users on History.UserId = Users.Id
                            INNER JOIN Actions on History.ActionId = Actions.Id
                            INNER JOIN Products on History.ProductId =  Products.Id;";

            var histories = new List<History>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                if(connection.State == System.Data.ConnectionState.Open)
                    histories = connection.Query<History>(sql).ToList();

                connection.Close();
            }

            return histories;
        }
        #endregion
    }
}
