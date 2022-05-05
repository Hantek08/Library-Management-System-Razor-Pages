using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using MySqlConnector;
using AgilSystemutveckling_Xamarin_Net5.TestModels;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.TestService.GetService
{
    public class Get
    {

        #region Connection string
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321";
        #endregion

        #region Product related methods
        public static List<TestModels.Product> GetAllProducts()
        {
            var sql = @$"SELECT Title, Author.AuthorName, 
                        Category.CategoryName, SubCategory.SubCategoryName
                        from Product
                        INNER JOIN Author ON Product.AuthorId = Author.Id
                        INNER JOIN Category ON Product.CategoryId = Category.Id
                        INNER JOIN SubCategory ON Product.SubCategoryId = SubCategory.Id";
            var name = new List<TestModels.Product>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<TestModels.Product>(sql).ToList();
                connection.Close();
            }

            return name;
        }
        #endregion

        #region User related methods
        public static List<TestModels.User> GetAllUsers()
        {
            var sql = @$"Select Username, Password from User";
            var user = new List<TestModels.User>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                user = connection.Query<TestModels.User>(sql).ToList();
            }

            return user;
        }

        public static List<TestModels.User> GetAllUsersOrderedAlphabetically()
        {
            var sql = @$"Select Username from User ORDER BY Username";
            var user = new List<TestModels.User>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                user = connection.Query<TestModels.User>(sql).ToList();
            }

            return user;
        }

        public static List<TestModels.User> GetAllUsersReverseOrder()
        {
            var sql = @$"Select Username from User ORDER BY Username desc";
            var user = new List<TestModels.User>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                user = connection.Query<TestModels.User>(sql).ToList();
            }

            return user;
        }

        public static List<TestModels.User> GetAllUsersStartingWithLetter()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            string letter = "<first letter of search query here>";
            var sql = @$"Select Username from User where Username = '{letter}%' ORDER BY Username";
            var user = new List<TestModels.User>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                user = connection.Query<TestModels.User>(sql).ToList();
            }

            return user;
        }
        #endregion
        #region Author related methods
        public static List<TestModels.Author> GetAllAuthors()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select Id, AuthorName 
                                From Author";
            var author = new List<TestModels.Author>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
               author = connection.Query<TestModels.Author>(sql).ToList();
            }

            return author;
        }
        public static List<TestModels.Author> GetAllAuthorsStartingWithLetter()
        {
            string letter = "<first letter of search query here>";
            var sql = @$"Select AuthorName from Author where AuthorName = '{letter}%' ORDER BY AuthorName";
            var author = new List<TestModels.Author>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                author = connection.Query<TestModels.Author>(sql).ToList();
            }

            return author;
        }

        public static List<TestModels.Author> GetAllAuthorsOrderedAlphabetically()
        {
            var sql = @$"Select AuthorName from Author ORDER BY AuthorName";
            var author = new List<TestModels.Author>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                author = connection.Query<TestModels.Author>(sql).ToList();
            }

            return author;
        }

        public static List<TestModels.Author> GetAllAuthorsReverseOrder()
        {
            var sql = @$"Select AuthorName from Author ORDER BY AuthorName desc";
            var author = new List<TestModels.Author>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                author = connection.Query<TestModels.Author>(sql).ToList();
            }

            return author;
        }

        #endregion
    }
}
