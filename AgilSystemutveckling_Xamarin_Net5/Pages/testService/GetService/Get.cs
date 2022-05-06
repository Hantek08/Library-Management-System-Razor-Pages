using AgilSystemutveckling_Xamarin_Net5.TestModels;
using AgilSystemutveckling_Xamarin_Net5.Constants;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.TestService.GetService
{
    public class Get
    {

        #region Connection string
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321";
        #endregion

        #region Product related methods
        public static List<Product> GetAllProducts()
        {
            string? sql = @$"SELECT Title, Author.AuthorName,
                        Category.CategoryName, SubCategory.SubCategoryName
                        from Product
                        INNER JOIN Author ON Product.AuthorId = Author.Id
                        INNER JOIN Category ON Product.CategoryId = Category.Id
                        INNER JOIN SubCategory ON Product.SubCategoryId = SubCategory.Id";
            var products = new List<Product>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                products = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return products;
        }

        public static List<Product> GetAllProductsOrderedAlphabetically()
        {
            string? sql = @$"SELECT Id, Title, AuthorName,
                        Category.CategoryName, SubCategory.SubCategoryName
                        from Product
                        INNER JOIN Author ON Product.AuthorId = Author.Id
                        INNER JOIN Category ON Product.CategoryId = Category.Id
                        INNER JOIN SubCategory ON Product.SubCategoryId = SubCategory.Id";
            var products = new List<Product>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                products = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return products;
        }

        public static List<Product> GetAllProductsOrderedByCategory()
        {
            var sql = @$"Select Id, Title, AuthorName 
                             from Product 
                             ORDER BY Category";
            var products = new List<Product>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                products = connection.Query<Product>(sql).ToList();
            }

            return products;
        }

        public static List<Product> GetAllProductsOrderedById()
        {
            var sql = @$"Select Id, Title, AuthorName 
                             from Author 
                             ORDER BY Id";
            var products = new List<Product>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                products = connection.Query<Product>(sql).ToList();
            }

            return products;
        }

        public static List<Product> GetAllProductsOrderedByIdReverse()
        {
            var sql = @$"Select Id, Title, AuthorName 
                             from Author 
                             ORDER BY Id desc";
            var products = new List<Product>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                products = connection.Query<Product>(sql).ToList();
            }

            return products;
        }
        #endregion

        #region User related methods
        public static List<TestModels.User> GetAllUsers()
        {
            var sql = @$"Select Username, Password
                            from User";
            var user = new List<User>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                user = connection.Query<User>(sql).ToList();
            }

            return user;
        }

        public static List<User> GetAllUsersOrderedAlphabetically()
        {
            var sql = @$"Select Username 
                            from User 
                            ORDER BY Username";
            var user = new List<User>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                user = connection.Query<User>(sql).ToList();
            }

            return user;
        }

        public static List<TestModels.User> GetAllUsersReverseOrder()
        {
            var sql = @$"Select Username from User ORDER BY Username desc";
            var user = new List<User>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                user = connection.Query<User>(sql).ToList();
            }

            return user;
        }

        public static List<User> GetAllUsersStartingWithLetter()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            string letter = "<first letter of search query here>";
            var sql = @$"Select Username from User where Username = '{letter}%' ORDER BY Username";
            var user = new List<User>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                user = connection.Query<User>(sql).ToList();
            }

            return user;
        }
        #endregion

        #region Author related methods
        public static List<Author> GetAllAuthors()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select Id, AuthorName 
                                From Author";
            var authors = new List<Author>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                authors = connection.Query<Author>(sql).ToList();
            }

            return authors;
        }
        public static List<Author> GetAllAuthorsStartingWithLetter()
        {
            string letter = "<first letter of search query here>";
            var sql = @$"Select AuthorName 
                            from Author 
                            where AuthorName = '{letter}%' 
                            ORDER BY AuthorName";
            var author = new List<Author>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Author>(sql).ToList();
            }

            return author;
        }

        public static List<Author> GetAllAuthorsOrderedAlphabetically()
        {
            var sql = @$"Select Id, AuthorName 
                             from Author 
                             ORDER BY AuthorName";
            var author = new List<Author>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Author>(sql).ToList();
            }

            return author;
        }

        public static List<Author> GetAllAuthorsReverseOrder()
        {
            var sql = @$"Select Id, AuthorName 
                            from Author 
                            ORDER BY AuthorName desc";
            var author = new List<Author>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Author>(sql).ToList();
            }

            return author;
        }

        public static List<Author> GetAllAuthorsOrderedById()
        {
            var sql = @$"Select Id, AuthorName 
                             from Author 
                             ORDER BY Id";
            var author = new List<Author>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Author>(sql).ToList();
            }

            return author;
        }

        public static List<Author> GetAllAuthorsReverseOrderId()
        {
            var sql = @$"Select Id, AuthorName 
                            from Author 
                            ORDER BY Id desc";
            var author = new List<Author>();
            using (var connection = new MySqlConnection(Constant.connectionString))
            {
                connection.Open();
                author = connection.Query<Author>(sql).ToList();
            }

            return author;
        }
        #endregion

    }
}
