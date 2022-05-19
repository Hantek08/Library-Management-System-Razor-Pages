using AgilSystemutveckling_Xamarin_Net5.Constants;
using AgilSystemutveckling_Xamarin_Net5.Models;
using Dapper;
using MySqlConnector;
using System.Data;
using System.Xml;

using static AgilSystemutveckling_Xamarin_Net5.Service.UpdateService.Update;
using static AgilSystemutveckling_Xamarin_Net5.Constants.Constant;
using static AgilSystemutveckling_Xamarin_Net5.Methods.Methods;
using static AgilSystemutveckling_Xamarin_Net5.Service.GetService.Get;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AgilSystemutveckling_Xamarin_Net5.Service.CreateService
{
    public class Create
    {

        #region User related
        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception"></exception>
        public static void AddUser(Users? user)
        {
            if (user != null)
            {
                CheckStringFormat(user.FirstName, user.LastName);

                int firstNameId = 0;
                int lastNameId = 0;
                int fullNameId = 0;

                bool firstNameExists = false;
                bool lastNameExists = false;

                var firstNames = GetAllFirstNames();
                if (firstNames != null)
                {
                    foreach (var item in firstNames)
                    {
                        if (item != null && user.FirstName == item.FirstName)
                        {
                            firstNameId = item.Id;
                            firstNameExists = true;
                            break;
                        }
                    }
                }
                else { throw new FormatException("First names cannot be null."); }

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
                if (lastNames != null)
                {
                    foreach (var item in lastNames)
                    {
                        if (user.LastName == item.LastName)
                        {
                            lastNameId = item.Id;
                            lastNameExists = true;
                            break;
                        }
                    }
                }
                else { throw new FormatException("Last names cannot be null."); }


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
            else { throw new FormatException("Cannot pass null User. Make sure a non-null user is passed."); }


        }

        /// <summary>
        /// Adds a user asynchronously to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Users?> AddUserAsync(Users? user)
        {
            if (user != null)
            {
                CheckStringFormat(user.FirstName, user.LastName);

                int fullNameId = 0;
                int firstNameId = 0;
                int lastNameId = 0;

                bool firstNameExists = false;
                bool lastNameExists = false;

                List<FirstNames?> firstNames = GetAllFirstNames();
                if (firstNames != null)
                {
                    foreach (var item in firstNames)
                    {
                        if (user.FirstName == item.FirstName)
                        {
                            firstNameId = item.Id;
                            firstNameExists = true;
                            break;
                        }
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
                                where FirstName = '{user.FirstName}'";

                    using (var connection = new MySqlConnection(ConnectionString))
                    {
                        await connection.OpenAsync();
                        if (connection.State == ConnectionState.Open)
                            firstNameId = connection.QuerySingleAsync<int>(sql2).Result;

                        await connection.CloseAsync();
                    }
                }

                List<LastNames?> lastNames = GetAllLastNames();
                foreach (var item in lastNames)
                {
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
                        await connection.ExecuteAsync(sql);
                        await connection.CloseAsync();
                    }

                    var sql2 = @$"SELECT Id
                                    FROM LastNames
                                    WHERE LastName = '{user.LastName}'";

                    using (var connection = new MySqlConnection(ConnectionString))
                    {
                        await connection.OpenAsync();
                        lastNameId = connection.QuerySingleAsync<int>(sql2).Result;
                        await connection.CloseAsync();
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
                        try
                        {
                            await connection.ExecuteAsync(sqlMain);
                            await connection.CloseAsync();
                        }
                        catch (Exception e) { throw new Exception("Could not add user.", e); }
                    }
                }

                return user;

            }
            else { throw new ArgumentNullException(nameof(user)); }

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
            if (product == null) { throw new ArgumentNullException(nameof(product)); }
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
                var categories = new List<Categories?>();
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        categories = connection.Query<Categories?>(sql).ToList();

                    connection.Close();
                }

                return categories;
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

            if (authors == null) { throw new ArgumentNullException(); }

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
            if (categories == null) { throw new ArgumentNullException(); }

            foreach (var category in categories)
            {
                if (category == null) { throw new ArgumentNullException(); }

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
            if (subCategories != null)
            {
                foreach (SubCategories? subCategory in subCategories)
                {
                    if (subCategory != null)
                    {
                        if (subCategory.SubCategoryName == product.SubCategoryName)
                        {
                            SubCategoryId = subCategory.Id;
                            SubCategoryExists = true;
                            break;
                        }
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
                                        where SubCategoryName = '{product.SubCategoryName}'";

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
                        try
                        {
                            connection.Execute(sqlMain);
                            connection.Close();
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Could not add Product, check if any inputs have < ' > signs.", e);
                        }


                }
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
                                VALUES (@CategoryName)";

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
            var cmdText = @$"INSERT INTO Categories (CategoryName)
                                    VALUES (@CategoryName)";

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


            var cmdText = @$"INSERT INTO SubCategory (SubCategoryName)
                                VALUES (@SubCategoryName)";

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

            var cmdText = @$"INSERT INTO SubCategory (SubCategoryName)
                                    VALUES (@SubCategoryName)";

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

        public static void AddLoan(int UserId, int ProductId, int ActionId)
        {
            DateTime today = DateTime.Today;



            var sqlMain = @$"INSERT INTO History (UserId, ProductId, Datetime, ActionId) 
                                    VALUES ({UserId}, {ProductId}, '{today.Year}-{today.Month}-{today.Day} 00:00:00', {ActionId})";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    try { connection.Execute(sqlMain); }
                    catch (Exception e) { throw new Exception("Could not add History.", e); }

                connection.Close();
            }

            Products? product = GetProductById(ProductId);
            if (product == null) { throw new NullReferenceException(); }

            int unitsInStock = product.UnitsInStock - 1;
            UpdateUnitsInStock(ProductId, unitsInStock);
        }

        #endregion
    }
}