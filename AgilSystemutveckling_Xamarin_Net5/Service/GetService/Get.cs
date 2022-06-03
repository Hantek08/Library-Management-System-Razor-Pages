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
        /*
         * Methods used to get lists with data from the database.
         * All the lists contains instances of a class found in
         * the Models folder.
         */

        #region Category related

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        public static List<Categories?>? GetAllCategories()
        {
            // Set sql query to the desired information to be fetched from MySQL database.
            string? sql = @$"SELECT Id, CategoryName
                                FROM Categories";

            // Using statement passing in mySqlConnection with connection string.
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // Open MySQL connection.
                connection.Open();

                // Check if connection state is open.
                if (connection.State == ConnectionState.Open)
                {
                    // Add mysql information to a new list.
                    var categories = connection.Query<Categories?>(sql).ToList();

                    // Close the current connection.
                    connection.Close();

                    // return list of items.
                    return categories;
                }
            }
            return null;
        }

        /// <summary>
        /// Get all categories asynchronously.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Categories?>?> GetAllCategoriesAsync()
        {
            // Set MySQL query.
            string? sql = @$"SELECT Id, CategoryName
                                FROM Categories";

            // Using statement passing in a MySQL connection instance.
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // Open connection asynchronously using await.
                await connection.OpenAsync();

                // Check if connection is open.
                if (connection.State == ConnectionState.Open)
                {
                    // Run query asynchronously, add to an IEnumerable collection.
                    var categories = await connection.QueryAsync<Categories?>(sql);

                    // Close connection asynchronously.
                    await connection.CloseAsync();

                    // Return list of items.
                    return categories.ToList();
                }
            }
            return null;
        }

        #endregion

        #region Subcategory related
        /// <summary>
        /// Gets all subcategories.
        /// </summary>
        /// <returns></returns>
        public static List<SubCategories?>? GetAllSubCategories()
        {
            string? sql = @$"SELECT Id, SubCategoryName 
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
        public static async Task<List<SubCategories?>?> GetAllSubCategoriesAsync()
        {
            string? sql = @$"SELECT Id, SubCategoryName 
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

        #region Product related methods

        /// <summary>
        /// Gets all products in a list of instances of Products.
        /// </summary>
        /// <returns></returns>
        public static List<Products?>? GetAllProducts()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl, Products.Active
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
        public static async Task<List<Products?>?> GetAllProductsAsync()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl, Products.Active
                            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    var products = connection.QueryAsync<Products?>(sql).Result;

                    await connection.CloseAsync();

                    return products.ToList();
                }

            }
            return null;
        }

        /// <summary>
        /// Gets a list of the most loaned products, excluding booked events and limited 
        /// by 'limitBy' parameter.
        /// The list is instances of the class Product found in Models folder.
        /// </summary>
        /// <param name="limitBy">The limit on popular products to get.</param>
        /// <returns></returns>
        public static List<Products?>? PopularProducts(int limitBy)
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id
                            where Categories.CategoryName != 'Event'
                            order by Id desc
                            limit {limitBy}";

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
        /// Gets specific product by ID. The object is an instance of the class Product found in Models folder.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Products? GetProductById(int id)
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl, Products.Active
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
        public static async Task<Products?>? GetProductByIdAsync(int id)
        {

            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl, Products.Active
                            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE Products.Id = {id}";

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
        /// The list is instances of the class Product found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static List<Products?>? GetAllProductsSortedByCategoryAsc()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl, Products.Active
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
        /// The list is instances of the class Product found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Products?>?> GetAllProductsSortedByCategoryAscAsync()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl, Products.Active
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
        /// The list is instances of the class Product found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static List<Products?>? GetAllProductsSortedByCategoryDesc()
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl, Products.Active
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
        public static async Task<List<Products?>?> GetAllProductsSortedByCategoryDescAsync()
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
        public static async Task<List<Products?>?> GetAllProductsSortedByAuthorAscAsync()
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
                    var products = await connection.QueryAsync<Products?>(sql);
                    await connection.CloseAsync();

                    return products.ToList();
                }
                return null;
            }
        }


        /// <summary>
        /// Gets all products that are marked as active/non-hidden.
        /// The list is instances of the class Product found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static List<Products?>? GetAllActiveProducts()
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
        public static async Task<List<Products?>?> GetAllActiveProductsAsync()
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
        /// The list is instances of the class Product found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static List<Products?>? GetAllInActiveProducts()
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
        public static async Task<List<Products?>?> GetAllInActiveProductsAsync()
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
        /// Gets all products by category, limited by in parameter 'CategoryName'.
        /// The list is populated by instances of the Products class.
        /// </summary>
        /// <returns></returns>
        public static List<Products?>? GetAllByCategory(string categoryName)
        {
            CheckStringFormat(categoryName);
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
				            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
				            Products.UnitsInStock, Products.InStock, Products.ImgUrl
				            FROM Products
                            INNER JOIN Authors on Products.AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id
                            WHERE Categories.CategoryName = '{categoryName}';";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var products = connection.Query<Products?>(sql).ToList();

                    connection.Close();
                    return products;
                }

                return null;
            }
        }

        /// <summary>
        /// Asynchronous version of GetAllByCategory.
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        public static async Task<List<Products?>?> GetAllByCategoryAsync(string CategoryName)
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
                    var products = await connection.QueryAsync<Products?>(sql);

                    await connection.CloseAsync();
                    return products.ToList();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets recently added products from the database.
        /// The list is instances of the class Product found in Models folder.
        /// </summary>
        /// <param name="CategoryName">Product category.</param>
        /// <param name="limitProductsBy">Number to limit recently added products by.</param>
        /// <returns></returns>
        public static List<Products?>? RecentlyAddedByCategory(string CategoryName, int limitProductsBy)
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

            var products = new List<Products?>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    products = connection.Query<Products?>(sql).ToList();
                    connection.Close();

                    return products;
                }
            }
            return products;
        }
        #endregion

        #region User related methods
        /// <summary>
        /// Gets all users in a list.
        /// The list is instances of the class Users found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static List<Users?>? GetAllUsers()
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
        public static async Task<List<Users?>?> GetAllUsersAsync()
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
        /// The list is instances of the class Users found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static List<Users?>? GetAllUsersOrderedAlphabetically()
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
        public static async Task<List<Users?>?> GetAllUsersOrderedAlphabeticallyAsync()
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
        /// The list is instances of the class Users found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static List<Users?>? GetAllUsersReverseOrder()
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
        public static async Task<List<Users?>?> GetAllUsersReverseOrderAsync()
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
        /// The list is instances of the class Users found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static List<Users?>? GetAllUsersStartingWithLetter(char a)
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
        public static async Task<List<Users?>?> GetAllUsersStartingWithLetterAsync(char a)
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

        //Gets all blocked users
        public static List<Users?>? AllBlockedUsers()
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
        public static List<Authors?>? GetAllAuthors()
        {
            var sql = @$"Select Id, AuthorName 
                                From Authors";

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

        /// <summary>
        /// Get all authors async.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Authors?>?> GetAllAuthorsAsync()
        {
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
        /// Get all authors ordered A-Ö.
        /// </summary>
        /// <returns></returns>
        public static List<Authors?>? GetAllAuthorsOrderedAlphabetically()
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

        /// <summary>
        /// Get all authors starts from Ö-A.
        /// </summary>
        /// <returns></returns>
        public static List<Authors?>? GetAllAuthorsReverseOrder()
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

        /// <summary>
        /// Gets all authors ordered by ID 1-9.
        /// </summary>
        /// <returns></returns>
        public static List<Authors?>? GetAllAuthorsOrderedById()
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

        /// <summary>
        /// Gets all authors in reversed ID order 9-1.
        /// </summary>
        /// <returns></returns>
        public static List<Authors?>? GetAllAuthorsReverseOrderId()
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
        /// The list is instances of the class FirstNames found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static List<FirstNames?>? GetAllFirstNames()
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
        public static async Task<List<FirstNames?>?> GetAllFirstNamesAsync()
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
        /// The list is instances of the class LastNames found in Models folder.
        /// </summary>
        /// <returns></returns>
        public static List<LastNames?>? GetAllLastNames()
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
        public static async Task<List<LastNames?>?> GetAllLastNamesAsync()
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
        /// Displays a list of all items with action ID 1 (lent to a user).
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<History?>? ActiveLoans(int userId)
        {
            var sql = $@"Select Title, Categories.CategoryName, Datetime, ProductId
                        FROM History
		                INNER JOIN Products on ProductId =  Products.Id
		                INNER JOIN Actions on ActionId = Actions.Id
                        INNER JOIN Categories on CategoryId = Categories.Id
                        INNER JOIN Users on UserId = Users.Id
                        WHERE ActionId = 1 And UserId = {userId}";



            List<History?>? historiesLent = new();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                     historiesLent = connection.Query<History?>(sql).ToList();

                    connection.Close();
                }
            }

            List<History?>? historiesReturned = new();

            var sql2 = $@"Select Title, Categories.CategoryName, Datetime
                        FROM History
		                INNER JOIN Products on ProductId =  Products.Id
		                INNER JOIN Actions on ActionId = Actions.Id
                        Inner Join Categories on CategoryId = Categories.Id
                        Inner Join Users on UserId = Users.Id

                        Where ActionId = 2 And UserId ={userId}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    historiesReturned = connection.Query<History?>(sql2).ToList();

                    connection.Close();


                }
            }

            List<History?>? activeLoans = new();

            bool isReturned;

            foreach (var lent in historiesLent)
            {
                isReturned = false;

                foreach (var returned in historiesReturned)
                {
                    if (lent.Title == returned.Title && lent.DateTime < returned.DateTime)
                    {
                        isReturned = true;
                        break;
                    }
                }
                // If a product is not returned, add to the list of active loans.
                if (!isReturned) { activeLoans.Add(lent); }
            }
            return activeLoans;
        }
        #endregion

        /// <summary>
        /// Displays a list of all items with action ID 7 (Booked to a user) with instances of Histories.
        /// Method to see Active bookings.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// 
        public static List<History?>? ActiveBookings()
        {
            var sql = $@"Select Products.Title, History.Datetime, History.ProductId, Actions.Action
                        FROM History
		                INNER JOIN Products on ProductId =  Products.Id
		                INNER JOIN Actions on ActionId = Actions.Id
                        INNER JOIN Users on UserId = Users.Id
                        INNER JOIN FullNames on FullNameId = FullNames.Id
                        INNER JOIN FirstNames on FirstNameId = FirstNames.Id
                        INNER JOIN LastNames on LastNameId = LastNames.Id
                        WHERE ActionId = 7";
           
            List<History?>? historiesBooked = new();
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    historiesBooked = connection.Query<History?>(sql).ToList();

                    connection.Close();


                }
            }

            var sqlLent = $@"Select Title, Datetime, ProductId
                        FROM History
		                INNER JOIN Products on ProductId =  Products.Id
		                INNER JOIN Actions on ActionId = Actions.Id
                        
                        INNER JOIN Users on UserId = Users.Id
                        WHERE ActionId = 1";



            List<History?>? historiesLent = new();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    historiesLent = connection.Query<History?>(sqlLent).ToList();

                    connection.Close();
                }
            }


            List<History?>? activeBookings = new();

            bool isLent;

            foreach (var booked in historiesBooked)
            {
                isLent = false;

                foreach (var lent in historiesLent)
                {
                    if (booked.Title == lent.Title && booked.DateTime < lent.DateTime)
                    {
                        isLent = true;
                        break;
                    }
                }
                // If a product is not loaned, add to the list of active bookings.
                if (!isLent) { activeBookings.Add(booked); }
            }
            return activeBookings;
        }

           
            #region History related
            /// <summary>
            /// Gets all histories.
            /// </summary>
            /// <returns></returns>
            public static List<History?>? GetAllHistories()
        {
            string? sql = @$"SELECT History.Id, FirstNames.FirstName, LastNames.LastName, Products.Title,
                            Actions.Action, History.Datetime, Categories.CategoryName
                            FROM History
                            INNER JOIN Users on History.UserId = Users.Id
                            INNER JOIN Actions on History.ActionId = Actions.Id
                            INNER JOIN Products on History.ProductId =  Products.Id
                            INNER JOIN Categories on Products.CategoryId =  Categories.Id
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
        public static async Task<List<History?>?> GetAllHistoriesAsync()
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
        public static List<History?>? LateReturns()
        {
            List<History?>? allHistories = GetAllHistories();

            if(allHistories is null) { throw new ArgumentNullException(nameof(allHistories)); }

            List<History?>? late = new();
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