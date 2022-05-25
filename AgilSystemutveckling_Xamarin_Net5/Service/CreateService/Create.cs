
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
            CheckIfObjectIsNull(user);

            CheckStringFormat(user.FirstName, user.LastName);

            int firstNameId = 0;
            int lastNameId = 0;
            int fullNameId = 0;

            bool firstNameExists = false;
            bool lastNameExists = false;

            var firstNames = GetAllFirstNames();
            if (firstNames == null) { throw new NullReferenceException(); }

            foreach (var item in firstNames)
            {
                if (item != null && user.FirstName == item.FirstName)
                {
                    firstNameId = item.Id;
                    firstNameExists = true;
                    break;
                }
            }

            if (firstNameExists == false)
            {

                var sql = @$"INSERT INTO FirstNames (FirstName)
                                    VALUES ('{user.FirstName}')";


                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        connection.Execute(sql);

                    connection.Close();
                }

                var sql2 = @$"SELECT Id
                                    FROM FirstNames
                                    WHERE FirstName = '{user.FirstName}'";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        firstNameId = connection.QuerySingle<int>(sql2);

                    connection.Close();
                }
            }

            var lastNames = GetAllLastNames();
            if (lastNames == null) { throw new NullReferenceException(); }

            foreach (var item in lastNames)
            {
                if (item == null) { throw new NullReferenceException(); }

                if (user.LastName == item.LastName)
                {
                    lastNameId = item.Id;
                    lastNameExists = true;
                    break;
                }
            }


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
                                  WHERE LastNameId = {lastNameId} and FirstNameId = {firstNameId}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    fullNameId = connection.QuerySingle<int>(sqlFN2);

                connection.Close();
            }

            CheckStringFormat(user.Username, user.Password, user.Address);

            var sqlMain = @$"INSERT INTO Users (FullNameId, Username, Password, AccessId, Address, Blocked) 
                                    VALUES ({fullNameId}, '{user.Username}', '{user.Password}', {user.Level}, '{user.Address}',
                                    {user.Blocked})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    try { connection.Execute(sqlMain); }
                    catch (Exception e) { throw new Exception("Could not add user.", e); }

                connection.Close();
            }
        }

        /// <summary>
        /// Adds a user asynchronously to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Users?> AddUserAsync(Users? user)
        {
            // Performs check if user passed is null.
            CheckIfObjectIsNull(user);

            // Performs check on strings in user for correct format and null.
            CheckStringFormat(user.FirstName, user.LastName);

            int fullNameId = 0;
            int firstNameId = 0;
            int lastNameId = 0;

            bool firstNameExists = false;
            bool lastNameExists = false;

            List<FirstNames?> firstNames = GetAllFirstNames();



            foreach (var item in firstNames)
            {
                CheckIfObjectIsNull(item);
                CheckStringFormat(item.FirstName);

                if (user.FirstName == item.FirstName)
                {
                    firstNameId = item.Id;
                    firstNameExists = true;
                    break;
                }
            }



            if (firstNameExists == false)
            {
                var sql = @$"INSERT INTO FirstNames (FirstName) 
                                    VALUES ('{user.FirstName}')";
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    if (connection.State == ConnectionState.Open)
                        await connection.ExecuteAsync(sql);

                    await connection.CloseAsync();
                }

                var sql2 = @$"SELECT Id
                                    FROM FirstNames
                                    WHERE FirstName = '{user.FirstName}'";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    if (connection.State == ConnectionState.Open)
                        firstNameId = connection.QuerySingleAsync<int>(sql2).Result;

                    await connection.CloseAsync();
                }
            }

            List<LastNames?> lastNames = GetAllLastNames();
            CheckIfObjectIsNull(lastNames);
            foreach (var item in lastNames)
            {
                CheckIfObjectIsNull(item);

                if (user.LastName == item.LastName)
                {
                    lastNameId = item.Id;
                    lastNameExists = true;
                    break;
                }
            }

            if (lastNameExists == false)
            {
                var sql = @$"INSERT INTO LastNames (LastName) 
                                    VALUES ('{user.LastName}')";
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    if (connection.State == ConnectionState.Open)
                        await connection.ExecuteAsync(sql);

                    await connection.CloseAsync();
                }

                var sql2 = @$"SELECT Id
                                    FROM LastNames
                                    WHERE LastName = '{user.LastName}'";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    if (connection.State == ConnectionState.Open)
                    {
                        lastNameId = connection.QuerySingleAsync<int>(sql2).Result;
                        await connection.CloseAsync();
                    }

                }
            }

            var sqlFN = @$"INSERT INTO FullNames (FirstNameId, LastNameId) 
                                VALUES ({firstNameId}, {lastNameId})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                    await connection.ExecuteAsync(sqlFN);

                await connection.CloseAsync();
            }

            var sqlFN2 = @$"SELECT Id
                                FROM FullNames
                                WHERE LastNameId = {lastNameId} and FirstNameId = {firstNameId}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                    fullNameId = connection.QuerySingleAsync<int>(sqlFN2).Result;

                await connection.CloseAsync();
            }

            CheckStringFormat(user.Username, user.Password, user.Address);

            var sqlMain = @$"INSERT INTO Users (FullNameId, Username, Password, AccessId, Address, Blocked) 
                                    VALUES ({fullNameId}, '{user.Username}', '{user.Password}', {user.Level}, '{user.Address}', {user.Blocked})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    // We can either use try for all executes or for none since CheckStringFormat will make sure the correct format is passed.
                    /*try
                    {*/
                    await connection.ExecuteAsync(sqlMain);
                    await connection.CloseAsync();
                    /*}
                    catch (Exception e) { throw new Exception("Could not add user.", e); }*/
                }
            }

            return user;




        }
        #endregion

        #region Author related
        /// <summary>
        /// Adds an author to the database.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public static void AddAuthor(Authors? author)
        {
            CheckIfObjectIsNull(author);
            CheckStringFormat(author.AuthorName);
            var sql = @$"INSERT INTO Authors (AuthorName)
                                VALUES (@{author.AuthorName})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    connection.Execute(sql);

                connection.Close();
            }
        }

        /// <summary>
        /// Adds an author asynchronously to the database.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public static async Task<Authors?> AddAuthorAsync(Authors? author)
        {
            CheckIfObjectIsNull(author);
            CheckStringFormat(author.AuthorName);
            var sql = @$"INSERT INTO Authors (AuthorName)
                                VALUES (@{author.AuthorName})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                    await connection.ExecuteAsync(sql);

                await connection.CloseAsync();
            }
            return author;
        }
        #endregion

        #region Product related
        public static void AddProduct(Products? product)
        {
            CheckIfObjectIsNull(product);

            CheckStringFormat(product.Description, product.CategoryName, product.SubCategoryName);

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
                        var categories = connection.Query<Categories?>(sql).ToList();

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
            }

            int AuthorId = 0;
            int CategoryId = 0;
            int SubCategoryId = 0;

            bool AuthorExists = false;
            bool CategoryExists = false;
            bool SubCategoryExists = false;

            List<Authors?> authors = GetAllAuthors();

            CheckIfObjectIsNull(authors);

            foreach (var author in authors)
            {
                CheckIfObjectIsNull(author);

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

            List<Categories?> categories = GetAllCategories();

            CheckIfObjectIsNull(categories);

            foreach (var category in categories)
            {
                CheckIfObjectIsNull(category);

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

            List<SubCategories?> subCategories = GetAllSubCategories();
            CheckIfObjectIsNull(subCategories);
            foreach (SubCategories? subCategory in subCategories)
            {
                CheckIfObjectIsNull(subCategory);

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

        /// <summary>
        /// Adds new category asynchronously to the database.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static async Task<Categories?> AddCategoryAsync(Categories? category)
        {
            CheckIfObjectIsNull(category);
            CheckStringFormat(category.CategoryName);

            var cmdText = @$"INSERT INTO Categories (CategoryName)
                                    VALUES ('{category.CategoryName}')";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                    await connection.ExecuteAsync(cmdText);

                await connection.CloseAsync();
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
        public static void AddSubCategory(SubCategories? subcategory)
        {
            CheckIfObjectIsNull(subcategory);
            CheckStringFormat(subcategory.SubCategoryName);

            var cmdText = @$"INSERT INTO SubCategory (SubCategoryName)
                                    VALUES ({subcategory.SubCategoryName})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    connection.Execute(cmdText);
            }
        }

        /// <summary>
        /// Adds new subcategory asynchronously to the database.
        /// </summary>
        /// <param name="subcategory"></param>
        /// <returns></returns>
        public static async Task<SubCategories?> AddSubCategoryAsync(SubCategories? subcategory)
        {
            CheckIfObjectIsNull(subcategory);
            CheckStringFormat(subcategory.SubCategoryName);

            var cmdText = @$"INSERT INTO SubCategory (SubCategoryName)
                                    VALUES ({subcategory.SubCategoryName})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                    await connection.ExecuteAsync(cmdText);
            }
            return subcategory;
        }
        #endregion

        #region Loan related

        public static void AddHistory(int UserId, int ProductId, int ActionId)
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

            Products? product = GetProductById(ProductId);
            CheckIfObjectIsNull(product);


            int unitsInStock = product.UnitsInStock - 1;
            UpdateUnitsInStock(ProductId, unitsInStock);
        }

        #endregion
    }
}