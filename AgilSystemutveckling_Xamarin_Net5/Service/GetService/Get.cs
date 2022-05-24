using AgilSystemutveckling_Xamarin_Net5.Models;
using Dapper;
using MySqlConnector;
using System.Data;

using static AgilSystemutveckling_Xamarin_Net5.Methods.Methods;
using static AgilSystemutveckling_Xamarin_Net5.Constants.Constant;

namespace AgilSystemutveckling_Xamarin_Net5.Service.GetService
{
    public static class Get
    {

        #region Category related

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        public static List<Categories?> GetAllCategories()
        {
            var sql = @$"SELECT *
                                FROM Categories";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var categories = connection.Query<Categories?>(sql).ToList();
                    connection.Close();

                    return categories;
                }
            }
            return null;
        }

        /// <summary>
        /// Get all categories asynchronously.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Categories?>> GetAllCategoriesAsync()
        {
            var sql = @$"SELECT *
                                FROM Categories";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var results = await connection.QueryAsync<Categories?>(sql);
                    await connection.CloseAsync();

                    return results.ToList();
                }
            }
            return null;
        }

        #region Subcategory related
        /// <summary>
        /// Gets all subcategories.
        /// </summary>
        /// <returns></returns>
        public static List<SubCategories?> GetAllSubCategories()
        {
            var sql = @$"SELECT * 
                                FROM SubCategories";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var subCategories = connection.Query<SubCategories?>(sql).ToList();
                    connection.Close();

                    return subCategories;
                }

                return null;
            }
        }

        /// <summary>
        /// Get all subcategories async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<SubCategories?>> GetAllSubCategoriesAsync()
        {
            var sql = @$"SELECT * 
                                FROM SubCategories";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var subCategories = await connection.QueryAsync<SubCategories?>(sql);
                    await connection.CloseAsync();

                    return subCategories.ToList();
                }

                return null;
            }
        }
        #endregion

        #endregion

        #region Product related methods

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns></returns>
        public static List<Products?> GetAllProducts()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var products = connection.Query<Products?>(sql).ToList();

                    connection.Close();

                    return products;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all products async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products?>> GetAllProductsAsync()
        {
            string? sql = @$"SELECT *
                            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    IEnumerable<Products?> products = connection.QueryAsync<Products?>(sql).Result;

                    await connection.CloseAsync();

                    return products.ToList();
                }

            }
            return null;
        }

        /// <summary>
        /// Gets specific product by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Products? GetProductById(int id)
        {
            string? sql = @$"SELECT *
                            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE Products.Id = {id}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var product = connection.QuerySingle<Products?>(sql);

                    connection.Close();

                    return product;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets specific product by ID async.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Products?> GetProductByIdAsync(int id)
        {

            string? sql = @$"SELECT *
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            where Products.Id = {id}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var product = await connection.QuerySingleAsync<Products?>(sql);

                    await connection.CloseAsync();

                    return product;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets all products sorted by category A-Ö.
        /// </summary>
        /// <returns></returns>
        public static List<Products?> GetAllProductsSortedByCategoryAsc()
        {
            string? sql = @$"SELECT *
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            ORDER BY Categories.Id;";


            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var products = connection.Query<Products?>(sql).ToList();

                    connection.Close();

                    return products;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all products sorted by category A-Ö async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products?>> GetAllProductsSortedByCategoryAscAsync()
        {
            string? sql = @$"SELECT *
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            ORDER BY Categories.Id";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var products = await connection.QueryAsync<Products?>(sql);

                    await connection.CloseAsync();

                    return products.ToList();
                }
            }

            return null;
        }


        /// <summary>
        /// Gets all products sorted by category Ö-A.
        /// </summary>
        /// <returns></returns>
        public static List<Products?> GetAllProductsSortedByCategoryDesc()
        {
            string? sql = @$"SELECT *
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            ORDER BY Categories.Id desc";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var products = connection.Query<Products?>(sql).ToList();
                    connection.Close();
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all products sorted by category Ö-A async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products?>> GetAllProductsSortedByCategoryDescAsync()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            ORDER BY Categories.Id desc";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var name = await connection.QueryAsync<Products?>(sql);
                    await connection.CloseAsync();

                    return name.ToList();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets all products sorted by author ascending async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products?>> GetAllProductsSortedByAuthorAscAsync()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            ORDER BY AuthorName";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var name = await connection.QueryAsync<Products?>(sql);
                    await connection.CloseAsync();

                    return name.ToList();
                }
                return null;
            }
        }


        /// <summary>
        /// Gets all products that are marked as active/non-hidden.
        /// </summary>
        /// <returns></returns>
        public static List<Products?> GetAllActiveProducts()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE Products.Active = 1";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var products = connection.Query<Products?>(sql).ToList();

                    connection.Close();

                    return products;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all products that are marked as active/non-hidden async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products?>> GetAllActiveProductsAsync()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE Products.Active = 1";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var products = await connection.QueryAsync<Products?>(sql);

                    await connection.CloseAsync();

                    return products.ToList();
                }
            }

            return null;
        }


        /// <summary>
        /// Gets all products that are marked as inactive/hidden.
        /// </summary>
        /// <returns></returns>
        public static List<Products?> GetAllInActiveProducts()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE Products.Active = 0";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var products = connection.Query<Products?>(sql).ToList();

                    connection.Close();

                    return products;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all products that are marked as inactive/hidden async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products?>> GetAllInActiveProductsAsync()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE Products.Active = 0";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var products = await connection.QueryAsync<Products?>(sql);

                    await connection.CloseAsync();

                    return products.ToList();
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a specific book(?)
        /// </summary>
        /// <returns></returns>
        public static Products? GetBook()
        {
            /*string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products
                            WHERE Categories = 'Book'";*/

            string? sql = $@"SELECT Title, AuthorId, SubCategoryId
                              FROM Products
                              WHERE Products.CategoryId = 1;";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var book = connection.QuerySingle<Products?>(sql);
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
        public static async Task<Products?> GetBookAsync()
        {
            /*string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products
                            WHERE Categories='Book'";*/

            // had issues running  above sql query and tried this instead

            string? sql = $@"SELECT Title, AuthorId, SubCategoryId
                              FROM Products
                              WHERE CategoryId = 1;";


            using var connection = new MySqlConnection(ConnectionString);
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    Products? book = await connection.QuerySingleAsync<Products?>(sql);
                    await connection.CloseAsync();
                    return book;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets all by category.
        /// </summary>
        /// <returns></returns>
        public static List<Products?> GetAllByCategory(string CategoryName)
        {
            CheckStringFormat(CategoryName);
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
				            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
				            Products.UnitsInStock, Products.InStock, Products.ImgUrl
				            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE Categories.CategoryName = '{CategoryName}';";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var seminars = connection.Query<Products?>(sql).ToList();

                    connection.Close();
                    return seminars;
                }

                return null;
            }
        }

        public static async Task<List<Products?>> GetAllByCategoryAsync(string CategoryName)
        {
            CheckStringFormat(CategoryName);
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
				            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
				            Products.UnitsInStock, Products.InStock, Products.ImgUrl
				            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE Categories.CategoryName = '{CategoryName}';";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var seminars = await connection.QueryAsync<Products?>(sql);

                    await connection.CloseAsync();
                    return seminars.ToList();
                }

                return null;
            }
        }

        public static List<Products?> RecentlyAddedByCategory(string CategoryName, int limitProductsBy)
        {
            CheckStringFormat(CategoryName);
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
				            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
				            Products.UnitsInStock, Products.InStock, Products.ImgUrl
				            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE Categories.CategoryName = '{CategoryName}'
                            order by Id desc limit {limitProductsBy};";

            var books = new List<Products?>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    books = connection.Query<Products?>(sql).ToList();
                    connection.Close();

                    return books;
                }
            }
            return books;
        }
        #endregion

        #region User related methods
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public static List<Users?> GetAllUsers()
        {
            var sql = @$"Select Users.Id, Users.Username, Users.Password, Users.Address, Users.Blocked, 
                        FirstNames.FirstName, LastNames.LastName, Access.Level
                        from Users
                        inner join FullNames on Users.FullNameId = FullNames.Id
                        inner join FirstNames on FullNames.FirstNameId = FirstNames.Id
                        inner join LastNames on FullNames.LastNameId = LastNames.Id
                        inner join Access on Users.AccessId = Access.Id";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var users = connection.Query<Users?>(sql).ToList();
                    connection.Close();

                    return users;
                }
            }
            return null;
        }
        /// <summary>
        /// Gets all users async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Users?>> GetAllUsersAsync()
        {
            var sql = @$"Select Users.Id, Users.Username, Users.Password, Users.Address, Users.Blocked, 
                        FirstNames.FirstName, LastNames.LastName, Access.Level
                        from Users
                        inner join FullNames on Users.FullNameId = FullNames.Id
                        inner join FirstNames on FullNames.FirstNameId = FirstNames.Id
                        inner join LastNames on FullNames.LastNameId = LastNames.Id
                        inner join Access on Users.AccessId = Access.Id";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var users = await connection.QueryAsync<Users?>(sql);
                    await connection.CloseAsync();

                    return users.ToList();
                }
                return null;
            }
        }
        /// <summary>
        /// Gets all users sorted A-Ö.
        /// </summary>
        /// <returns></returns>
        public static List<Users?> GetAllUsersOrderedAlphabetically()
        {
            var sql = @$"Select Username 
                            FROM Users
                            ORDER BY Username";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var users = connection.Query<Users?>(sql).ToList();
                    connection.Close();
                    return users;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets all users sorted A-Ö async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Users?>> GetAllUsersOrderedAlphabeticallyAsync()
        {
            var sql = @$"Select Username 
                            FROM Users
                            ORDER BY Username";
            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var users = await connection.QueryAsync<Users?>(sql);
                    await connection.CloseAsync();

                    return users.ToList();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets all users sorted Ö-A.
        /// </summary>
        /// <returns></returns>
        public static List<Users?> GetAllUsersReverseOrder()
        {
            var sql = @$"Select Username 
                                FROM Users 
                                ORDER BY Username desc";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var user = connection.Query<Users?>(sql).ToList();
                    connection.Close();

                    return user;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets all users sorted Ö-A async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Users?>> GetAllUsersReverseOrderAsync()
        {
            var sql = @$"Select Username 
                                FROM Users 
                                ORDER BY Username desc";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var users = await connection.QueryAsync<Users?>(sql);
                    await connection.CloseAsync();

                    return users.ToList();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets all users sorted depending on a specific character.
        /// </summary>
        /// <returns></returns>
        public static List<Users?> GetAllUsersStartingWithLetter(char a)
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"SELECT Username 
                               FROM Users 
                               WHERE Username = '{a}%'
                               ORDER BY Username";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var users = connection.Query<Users?>(sql).ToList();
                    connection.Close();
                    return users;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets all users sorted depending on a specific character async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Users?>> GetAllUsersStartingWithLetterAsync(char a)
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.

            if (char.IsLetter(a))
            {
                var sql = @$"SELECT Username 
                               FROM Users 
                               WHERE Username = '{a}%'
                               ORDER BY Username";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    if (connection.State == ConnectionState.Open)
                    {
                        var users = await connection.QueryAsync<Users?>(sql);
                        await connection.CloseAsync();

                        return users.ToList();
                    }
                }

            }
            else { throw new FormatException("A letter must be passed to the method."); }

            return null;
        }

        public static List<Users?> AllBlockedUsers()
        {
            var sql = @"SELECT *
                            FROM Users
                            Where Blocked = '1';";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var users = connection.Query<Users?>(sql);
                    connection.Close();

                    return users.ToList();
                }
            }

            return null;

        }
        #endregion

        #region Author related methods
        /// <summary>
        /// Get all authors.
        /// </summary>
        /// <returns></returns>
        public static List<Authors?> GetAllAuthors()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select Id, AuthorName 
                                From Authors";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var authors = connection.Query<Authors?>(sql);
                    connection.Close();

                    return authors.ToList();

                }

                return null;
            }
        }

        /// <summary>
        /// Get all authors async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Authors?>> GetAllAuthorsAsync()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select Id, AuthorName 
                                From Authors";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var authors = await connection.QueryAsync<Authors?>(sql);

                    await connection.CloseAsync();

                    return authors.ToList();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets all authors starting with a specific letter
        /// </summary>
        /// <returns></returns>
        public static List<Authors?> GetAllAuthorsStartingWithLetter(char letter)
        {
            // checks if string is a letter 

            if (char.IsLetter(letter))
            {
                var sql = @$"Select AuthorName
                                FROM Author
                                WHERE AuthorName = '{letter}%'
                                ORDER BY AuthorName";


                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        var authors = connection.Query<Authors?>(sql).ToList();
                        connection.Close();

                        return authors;
                    }
                    return null;
                }
            }
            else { throw new FormatException("A letter must be passed in the method."); }

        }
        public static List<Authors?> GetAllAuthorsOrderedAlphabetically()
        {
            var sql = @$"Select Id, AuthorName 
                             from Authors 
                             ORDER BY AuthorName";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var authors = connection.Query<Authors?>(sql);
                    connection.Close();

                    return authors.ToList();
                }
            }
            return null;
        }

        public static List<Authors?> GetAllAuthorsReverseOrder()
        {
            var sql = @$"Select Id, AuthorName 
                            from Authors 
                            ORDER BY AuthorName desc";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var authors = connection.Query<Authors?>(sql);
                    connection.Close();

                    return authors.ToList();
                }

            }
            return null;
        }

        public static List<Authors?> GetAllAuthorsOrderedById()
        {
            var sql = @$"Select Id, AuthorName 
                             from Authors
                             ORDER BY Id";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var authors = connection.Query<Authors?>(sql);
                    connection.Close();

                    return authors.ToList();

                }
                return null;
            }
        }

        public static List<Authors?> GetAllAuthorsReverseOrderId()
        {
            var sql = @$"Select Id, AuthorName 
                            from Authors
                            ORDER BY Id desc";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var authors = connection.Query<Authors?>(sql);
                    connection.Close();

                    return authors.ToList();
                }

                return null;
            }
        }

        #endregion

        #region Names related

        /// <summary>
        /// Gets all first names from FirstNames table.
        /// </summary>
        /// <returns></returns>
        public static List<FirstNames?> GetAllFirstNames()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"SELECT Id, FirstName
                                FROM FirstNames";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var firstNames = connection.Query<FirstNames?>(sql).ToList();

                    connection.Close();
                    return firstNames;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets all first names from FirstNames table async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<FirstNames?>> GetAllFirstNamesAsync()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"SELECT Id, FirstName
                                FROM FirstNames";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var firstNames = await connection.QueryAsync<FirstNames?>(sql);

                    await connection.CloseAsync();

                    return firstNames.ToList();
                }
            }
            return null;
        }



        /// <summary>
        /// Gets all last names from LastNames table.
        /// </summary>
        /// <returns></returns>
        public static List<LastNames?> GetAllLastNames()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select Id, LastName
                                From LastNames";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var lastName = connection.Query<LastNames?>(sql).ToList();
                    connection.Close();

                    return lastName;
                }

            }
            return null;
        }

        /// <summary>
        /// Gets all last names from LastNames table async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<LastNames?>> GetAllLastNamesAsync()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select Id, LastName
                                From LastNames";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var lastName = await connection.QueryAsync<LastNames?>(sql);
                    await connection.CloseAsync();

                    return lastName.ToList();
                }

            }
            return null;
        }


        #endregion

        #region Loan related
        /// <summary>
        /// Returns number of loans a customer has made.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int NumberOfItemsLentToCustomer(Users? user)
        {
            var sql = $@"SELECT *
                                FROM History
                                WHERE History.UserId = @Users.Id;";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var products = connection.Query<Products?>(sql).ToList();
                    connection.Close();

                    return products.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Show Active loans.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<History?> ActiveLoans(int userId)
        {
            var sql = $@"Select Title, Categories.CategoryName, Datetime
                FROM History
		        INNER JOIN Products on ProductId =  Products.Id
		        INNER JOIN Actions on ActionId = Actions.Id
                Inner Join Categories on CategoryId = Categories.Id
                Inner Join Users on UserId = Users.Id

                Where ActionId = 1 And UserId ={userId}";

            var sql2 = $@"Select Title, Categories.CategoryName, Datetime
                FROM History
		        INNER JOIN Products on ProductId =  Products.Id
		        INNER JOIN Actions on ActionId = Actions.Id
                Inner Join Categories on CategoryId = Categories.Id
                Inner Join Users on UserId = Users.Id

                Where ActionId = 2 And UserId ={userId}";


            var historiesLoaned = new List<History>();

            var historiesReturned = new List<History>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                     historiesLoaned = connection.Query<History?>(sql).ToList();

                    connection.Close();

                    
                }
            }

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    historiesReturned = connection.Query<History?>(sql2).ToList();

                    connection.Close();


                }
            }
            return null;
        }
        #endregion

        #region History related
        /// <summary>
        /// Gets all histories.
        /// </summary>
        /// <returns></returns>
        public static List<History?> GetAllHistories()
        {
            string? sql = @$"SELECT History.Id, FirstNames.FirstName, LastNames.LastName, Products.Title, Actions.Action, History.Datetime
                            FROM History
                            INNER JOIN Users on History.UserId = Users.Id
                            INNER JOIN Actions on History.ActionId = Actions.Id
                            INNER JOIN Products on History.ProductId =  Products.Id
                            INNER JOIN FullNames on Users.FullNameId = FullNames.Id
                            INNER JOIN FirstNames on FullNames.FirstNameId = FirstNames.Id
                            INNER JOIN LastNames on FullNames.LastNameId = LastNames.Id;";


            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var histories = connection.Query<History?>(sql).ToList();
                    connection.Close();

                    return histories;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets all histories.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<History?>> GetAllHistoriesAsync()
        {
            string? sql = @$"SELECT History.Id, FirstNames.FirstName, LastNames.LastName, Products.Title, Actions.Action, History.Datetime
                            FROM History
                            INNER JOIN Users on UserId = Users.Id
                            INNER JOIN Actions on ActionId = Actions.Id
                            INNER JOIN Products on ProductId =  Products.Id
                            INNER JOIN FullNames on Users.FullNameId = FullNames.Id
                            INNER JOIN FirstNames on FullNames.FirstNameId = FirstNames.Id
                            INNER JOIN LastNames on FullNames.LastNameId = LastNames.Id;";


            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var histories = await connection.QueryAsync<History?>(sql);
                    await connection.CloseAsync();

                    return histories.ToList();
                }
            }

            return null;
        }


        /// <summary>
        /// Gets all late returns.
        /// </summary>
        /// <returns></returns>
        public static List<History?> LateReturns()
        {
            var allHistories = GetAllHistories();
            List<History?> late = new List<History?>();
            if (late != null)
            {
                foreach (History? hist in allHistories)
                {
                    if (hist != null)
                    {
                        if (hist.DateTime <= DateTime.Today.AddDays(-14))
                        {
                            late.Add(hist);
                        }
                    }
                }
            }
            return late;
        }
        #endregion
    }
}

/*Products.Id, Products.Title, Products.Description,
Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
Products.UnitsInStock, Products.InStock, Products.ImgUrl*/