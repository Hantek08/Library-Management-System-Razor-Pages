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


        public static List<Product> GetAllProducts()
        {
            string? sql = @$"Select Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitInStock, Products.InStock, Products.ImgUrl
                            from Products
                            inner join Authors on Products.AuthorId = Authors.Id
                            inner join Categories on Products.CategoryId = Categories.Id
                            inner join SubCategories on Products.SubCategoryId = SubCategories.Id";
            var name = new List<Product>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return name;
        }

        public static List<Product> GetBook()
        {
            string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products
                            WHERE Categories='Book'";
                        
            var name = new List<Product>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return name;
        }

        public static List<Product> GetEbook()
        {
            string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products
                            WHERE Categories='E-Book'";

            var name = new List<Product>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return name;
        }
        public static List<Product> GetMovie()
        {
            string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products 
                            WHERE Categories='Movie'";

            var name = new List<Product>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return name;
        }

        public static List<Product> BookSeminarium()
        {
            string? sql = @$"SELECT Title
                            FROM Products
                            WHERE Categories='Seminarium'";

            var name = new List<Product>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return name;
        }
        #endregion

        #region User related methods
        public static List<TestModels.User> GetAllUsers()
        {
            var sql = @$"Select Users.Id, Users.Username, Users.Password, Users.Address, Users.Blocked, 
                        FirstNames.FirstName, LastNames.LastName, Access.Level
                        from Users
                        inner join FullNames on Users.FullNameId = FullNames.Id
                        inner join FirstNames on FullNames.FirstNameId = FirstNames.Id
                        inner join LastNames on FullNames.LastNameId = LastNames.Id
                        inner join Access on Users.AccessId = Access.Id";
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
                            from Users
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
            var sql = @$"Select Username from Users ORDER BY Username desc";
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
            var sql = @$"Select Username from Users where Username = '{letter}%' ORDER BY Username";
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
        public static List<Models.Author> GetAllAuthors()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select Id, AuthorName 
                                From Authors";
            var author = new List<Models.Author>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                author = connection.Query<Models.Author>(sql).ToList();
            }

            return author;
        }
        public static List<Models.Author> GetAllAuthorsStartingWithLetter()
        {
            string letter = "<first letter of search query here>";
            var sql = @$"Select AuthorName 
                            from Author 
                            where AuthorName = '{letter}%' 
                            ORDER BY AuthorName";
            var author = new List<Models.Author>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                author = connection.Query<Models.Author>(sql).ToList();
            }

            return author;
        }
        public static List<Models.Author> GetAllAuthorsOrderedAlphabetically()
        {
            var sql = @$"Select Id, AuthorName 
                             from Authors 
                             ORDER BY AuthorName";
            var author = new List<Models.Author>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                author = connection.Query<Models.Author>(sql).ToList();
            }

            return author;
        }

        public static List<Models.Author> GetAllAuthorsReverseOrder()
        {
            var sql = @$"Select Id, AuthorName 
                            from Authors 
                            ORDER BY AuthorName desc";
            var author = new List<Models.Author>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                author = connection.Query<Models.Author>(sql).ToList();
            }

            return author;
        }

        public static List<Models.Author> GetAllAuthorsOrderedById()
        {
            var sql = @$"Select Id, AuthorName 
                             from Authors
                             ORDER BY Id";
            var author = new List<Models.Author>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                author = connection.Query<Models.Author>(sql).ToList();
            }

            return author;
        }

        public static List<Models.Author> GetAllAuthorsReverseOrderId()
        {
            var sql = @$"Select Id, AuthorName 
                            from Authors
                            ORDER BY Id desc";
            var author = new List<Models.Author>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                author = connection.Query<Models.Author>(sql).ToList();
            }

            return author;
        }

        #endregion

        #region Names related
        public static List<Models.FirstNames> GetAllFirstNames()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select FirstName
                                From FirstNames";
            var firstName = new List<Models.FirstNames>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                firstName = connection.Query<Models.FirstNames>(sql).ToList();
            }

            return firstName;
        }

        public static List<Models.LastNames> GetAllLastNames()
        {
            // If someone just wants to search for any user starting with a letter, this will
            // sort based on the first letter of the username.
            var sql = @$"Select * 
                                From LastNames";
            var lastName = new List<Models.LastNames>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                lastName = connection.Query<Models.LastNames>(sql).ToList();
            }

            return lastName;
        }

        #endregion

        #region Loan 
        public static void UserAction (int userID, int productID,int actionID) //vilken siffra ska det stå här för actionID när man lånar bok?
        {
            MySqlConnection connection = new MySqlConnection(connString);

            var cmdText = @$"INSERT INTO history (UserID, ProductID, DateTime, ActionID)
                                VALUES (@UserID, @ProductID, @DateTime, @ActionID)";

            var cmd = new MySqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue($"@UserID", userID);
            cmd.Parameters.AddWithValue($"@ProductID", productID);
            cmd.Parameters.AddWithValue($"@DateTime", DateTime.Now);
            cmd.Parameters.AddWithValue($"@ActionID", actionID);

            connection.Open();
            int r = cmd.ExecuteNonQuery();
            connection.Close();
        }
        #endregion
    }
}
