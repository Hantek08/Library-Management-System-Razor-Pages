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
        public static void AddUser(Users user)
        {
            int fullNameId = 0;
            int firstNameId = 0;
            int lastNameId = 0;

            bool firstNameExists = false;
            bool lastNameExists = false;
            
            List<FirstNames> firstNames = GetService.Get.GetAllFirstNames();
            foreach (var item in firstNames)
            {
                if (user.FirstName == item.FirstName)
                {
                    firstNameId = item.Id;
                    firstNameExists = true;
                    break;
                }
            }

            if (firstNameExists == false) 
            {
                var sql = @$"insert into FirstNames (FirstName) 
                        values ('{user.FirstName}')";
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                }

                var sql2 = @$"SELECT Id
                                FROM FirstNames
                                where FirstName = '{user.FirstName}'";

                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    firstNameId = connection.QuerySingle<int>(sql2);
                }
            }

            List<LastNames> lastNames = GetService.Get.GetAllLastNames();
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
                var sql = @$"insert into LastNames (LastName) 
                        values ('{user.LastName}')";
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                }

                var sql2 = @$"SELECT Id
                                FROM LastNames
                                where LastName = '{user.LastName}'";

                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    lastNameId = connection.QuerySingle<int>(sql2);
                }
            }

            var sqlFN = @$"insert into FullNames (FirstNameId, LastNameId) 
                        values ({firstNameId}, {lastNameId})";
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sqlFN);
            }

            var sqlFN2 = @$"SELECT Id
                            FROM FullNames
                            where LastNameId = {lastNameId} and FirstNameId = {firstNameId}";

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                fullNameId = connection.QuerySingle<int>(sqlFN2);
            }

            var sqlMain = @$"insert into Users (FullNameId, Username, Password, AccessId,
                                                    Address, Blocked) 
                        values ({fullNameId}, '{user.Username}', '{user.Password}', {user.Level}, '{user.Address}',
                                {user.Blocked})";
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                try
                {
                    connection.Execute(sqlMain);
                }
                catch (Exception e)
                {

                    throw new Exception("Could not add user.", e);
                }
            }

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
            static List<Authors> GetAllAuthors()
            {
                
                var sql = @$"Select Id, AuthorName 
                                From Authors";
                var author = new List<Models.Authors>();
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    author = connection.Query<Models.Authors>(sql).ToList();
                }

                return author;
            }

            static List<Models.Categories> GetAllCategories()
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

            List<Models.Authors> authors = GetAllAuthors();
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
                    try
                    {
                        connection.Execute(sqlMain);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Could not add Product, check if any inputs have < ' > signs.", e);
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
