
using AgilSystemutveckling_Xamarin_Net5.Models;
using Dapper;
using MySqlConnector;
using System.Data;

using static AgilSystemutveckling_Xamarin_Net5.Constants.Constant;
using static AgilSystemutveckling_Xamarin_Net5.Methods.Methods;
using static AgilSystemutveckling_Xamarin_Net5.Service.GetService.Get;
using static AgilSystemutveckling_Xamarin_Net5.Service.UpdateService.Update;

namespace AgilSystemutveckling_Xamarin_Net5.Service.CreateService
{
    public static class Create
    {


        #region User related
        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception"></exception>
        public static void AddUser(Users? user)
        {
            // Calls method that checks if the variable passed in is null.
            if(user is null) { throw new ArgumentNullException(nameof(user)); }

            // Calls method that checks if passed in variable properties are null, correctly formatted for SQL or short enough.
            CheckStringFormat(user.FirstName, user.LastName);

            // Declaration of temporary variables.
            int firstNameId = 0;
            int lastNameId = 0;
            int fullNameId = 0;

            // Declaration of bool variables.
            bool firstNameExists = false;
            bool lastNameExists = false;

            // Get the full list of first names to compare with the newly entered one from Front end.
            var firstNames = GetAllFirstNames();

            // Check if the list is null.
            if(firstNames is null) { throw new NullReferenceException(nameof(firstNames)); }

            // For each first name, check if it is null or if it already exists in database.
            foreach (var item in firstNames)
            {
                if (item != null && user.FirstName == item.FirstName)
                {
                    // Set the firstNameId to be the same as item ID, then break to the next statement.
                    firstNameId = item.Id;
                    firstNameExists = true;
                    break;
                }
            }

            // If first name does not exist in the database, add it using INSERT.
            if (firstNameExists == false)
            {

                var sql = @$"INSERT INTO FirstNames (FirstName)
                                    VALUES ('{user.FirstName}')";

                // Using MySQLConnection with connection string found in Constants folder.
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    // Open connection.
                    connection.Open();
                    // Check whether connection state is open.
                    if (connection.State == ConnectionState.Open)
                        connection.Execute(sql); // Execute SQL query.

                    // Close connection before leaving to the next statement.
                    connection.Close();
                }
                // Get the Id of the first name the current user has.
                var sql2 = @$"SELECT Id
                                    FROM FirstNames
                                    WHERE FirstName = '{user.FirstName}'";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    // Open connection.
                    connection.Open();
                    // Check whether connection state is open.
                    if (connection.State == ConnectionState.Open)
                        firstNameId = connection.QuerySingle<int>(sql2); // Execute single-row query.

                    // Close connection before leaving to the next statement.
                    connection.Close();
                }
            }

            // Get all last names from database.
            var lastNames = GetAllLastNames();

            // Check whether the list is null.
            if (lastNames == null) { throw new NullReferenceException(); }

            // For each last name, check if it is null, and compare the user last name to the last names already present in database.
            foreach (var item in lastNames)
            {
                // Check for null.
                if (item == null) { throw new NullReferenceException(); }

                if (user.LastName == item.LastName)
                {
                    // Set current last name ID to the existing last name's ID, then exit to the next statement, avoiding duplicates in database.
                    lastNameId = item.Id;
                    lastNameExists = true;
                    break;
                }
            }

            // If last name was not matched, add it to the database using INSERT.
            if (lastNameExists == false)
            {
                var sql = @$"INSERT INTO LastNames (LastName) 
                                    VALUES ('{user.LastName}')";


                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        connection.Execute(sql);

                    connection.Close();
                }

                var sql2 = @$"SELECT Id
                                    FROM LastNames
                                    WHERE LastName = '{user.LastName}'";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        lastNameId = connection.QuerySingle<int>(sql2);

                    connection.Close();
                }
            }

            // Query for adding a new FullNames object using first name and last name ID's.
            var sqlFN = @$"INSERT INTO FullNames (FirstNameId, LastNameId)
                                  VALUES ({firstNameId}, {lastNameId})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    connection.Execute(sqlFN);

                connection.Close();
            }

            var sqlFN2 = @$"SELECT Id
                                  FROM FullNames
                                  WHERE LastNameId = {lastNameId} AND FirstNameId = {firstNameId}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    fullNameId = connection.QuerySingle<int>(sqlFN2);

                connection.Close();
            }

            // Perform format check for user string properties.
            CheckStringFormat(user.Username, user.Password, user.Address);

            // Query for adding a user.
            var sqlMain = @$"INSERT INTO Users (FullNameId, Username, Password, AccessId, Address, Blocked) 
                                    VALUES ({fullNameId}, '{user.Username}', '{user.Password}', {user.Level}, '{user.Address}', {user.Blocked})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    try { connection.Execute(sqlMain); }
                    catch (Exception e) { throw new Exception("Could not add user.", e); }

                connection.Close();
            }
        }

        #endregion

        #region Author related

/* Can be removed 
 
        /// <summary>
        /// Adds an author to the database.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public static void AddAuthor(Authors? author)
        {
            CheckIfObjectIsNull(author);
            CheckStringFormat(author.AuthorName);

            // Add author using INSERT.
            var sql = @$"INSERT INTO Authors (AuthorName)
                                VALUES ({author.AuthorName})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    connection.Execute(sql);

                connection.Close();
            }
        }
*/
        #endregion

        #region Product related
        /// <summary>
        /// Adds a product to the library database.
        /// </summary>
        /// <param name="product"></param>
        public static void AddProduct(Products? product)
        {
            if(product is null) throw new ArgumentNullException(nameof(product));

            CheckStringFormat(product.Description, product.CategoryName, product.SubCategoryName);
/*
            // Get all authors using method.
            static List<Authors?> GetAllAuthors()
            {

                var sql = @$"Select Id, AuthorName 
                                From Authors";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        var author = connection.Query<Authors?>(sql).ToList();
                        connection.Close();

                        return author;
                    }
                }
                return null;
            }

            static List<Categories?> GetAllCategories()
            {
                var sql = @$"SELECT Id, CategoryName 
                                    FROM Categories";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        var categories = connection.Query<Categories?>(sql);

                        connection.Close();

                        return categories.ToList();
                    }
                }
                return null;
            }

            static List<SubCategories?> GetAllSubCategories()
            {
                var sql = @$"SELECT Id, SubCategoryName 
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
                }
                return null;
            }*/

            int AuthorId = 0;
            int CategoryId = 0;
            int SubCategoryId = 0;

            bool AuthorExists = false;
            bool CategoryExists = false;
            bool SubCategoryExists = false;

            List<Authors?>? authors = GetAllAuthors();

            if(authors is null) { throw new ArgumentNullException(nameof(authors)); }

            foreach (var author in authors)
            {
                if(author is null) { throw new NullReferenceException(nameof(author)); }

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

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        connection.Execute(sql);

                    connection.Close();
                }

                var sql2 = @$"SELECT Id
                                    FROM Authors
                                    WHERE AuthorName = '{product.AuthorName}'";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        AuthorId = connection.QuerySingle<int>(sql2);

                    connection.Close();
                }
            }

            List<Categories?>? categories = GetAllCategories();

            if(categories is null) { throw new ArgumentNullException(nameof(categories)); } 

            foreach (var category in categories)
            {
                if(category is null) { throw new NullReferenceException(nameof(category)); }

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

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        connection.Execute(sql);

                    connection.Close();
                }

                var sql2 = @$"SELECT Id
                                    FROM Categories
                                    WHERE CategoryName = '{product.CategoryName}'";

                using (var connection = new MySqlConnection(ConnectionString))
                {

                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        CategoryId = connection.QuerySingle<int>(sql2);

                    connection.Close();
                }
            }

            List<SubCategories?>? subCategories = GetAllSubCategories();
            if(subCategories is null) { throw new ArgumentNullException(nameof(subCategories)); }
            foreach (SubCategories? subCategory in subCategories)
            {
                if (subCategory is null) { throw new NullReferenceException(nameof(subCategory)); }

                if (subCategory.SubCategoryName == product.SubCategoryName)
                {
                    SubCategoryId = subCategory.Id;
                    SubCategoryExists = true;
                    break;
                }

            }

            if (SubCategoryExists == false)
            {
                var sql = @$"INSERT INTO SubCategories (SubCategoryName) 
                                    VALUES ('{product.SubCategoryName}')";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        connection.Execute(sql);

                    connection.Close();
                }

                var sql2 = @$"SELECT Id
                                    FROM SubCategories
                                    WHERE SubCategoryName = '{product.SubCategoryName}'";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        SubCategoryId = connection.QuerySingle<int>(sql2);

                    connection.Close();
                }
            }

            CheckStringFormat(product.Title, product.Description, product.ImgUrl);

            var sqlMain = @$"INSERT INTO Products (Title, Description, AuthorId, CategoryId, SubCategoryId, UnitsInStock, ImgUrl) 
                                    VALUES ('{product.Title}', '{product.Description}', {AuthorId}, {CategoryId}, {SubCategoryId}, {product.UnitsInStock}, '{product.ImgUrl}')";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    connection.Execute(sqlMain);

                connection.Close();
            }
        }

        #endregion

        #region Category related
        /// <summary>
        /// Adds new category to the database.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static void AddCategory(Categories? category)
        {
            if(category is null) { throw new ArgumentNullException(nameof(category)); }

            CheckStringFormat(category.CategoryName);

            var cmdText = @$"INSERT INTO Categories (CategoryName)
                                    VALUES ('{category.CategoryName}')";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    connection.Execute(cmdText);

                connection.Close();
            }
        }

        #endregion

        #region Subcategory related
        /// <summary>
        /// Adds a new subcategory to the database.
        /// </summary>
        /// <param name="subcategory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddSubCategory(SubCategories? subcategory)
        {
            if(subcategory is null) { throw new ArgumentNullException(nameof(subcategory)); }
            CheckStringFormat(subcategory.SubCategoryName);

            var cmdText = @$"INSERT INTO SubCategory (SubCategoryName)
                                    VALUES ({subcategory.SubCategoryName})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    connection.Execute(cmdText);

                connection.Close();
            }
        }
        #endregion

        #region Loan related
        /// <summary>
        /// Adds a new history related to a user id, using a product id and specifies the action performed by a user on a product.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ProductId"></param>
        /// <param name="ActionId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static void AddHistory(int UserId, int ProductId, int ActionId)
        {
            // Check if id is a positive integer.
            if (UserId > -1 || ProductId > -1 || ActionId > -1)
            {

                var sqlMain = @$"INSERT INTO History (UserId, ProductId, Datetime, ActionId)
                                    VALUES ({UserId}, {ProductId}, '{DateTime.Now}', {ActionId})";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        connection.Execute(sqlMain);

                    connection.Close();
                }

                // Get product using method that takes product ID.
                Products? product = GetProductById(ProductId);

                if (product is null) { throw new ArgumentNullException(nameof(product)); }

                // ActionID 1 corresponds to loan.
                if (ActionId == 1)
                {
                    // Remove unit that is loaned from the stock.
                    int unitsInStock = product.UnitsInStock - 1;
                    UpdateUnitsInStock(ProductId, unitsInStock);
                }

            } else { throw new Exception("ID's cannot be less than 0."); }
        }
        #endregion
    }
}