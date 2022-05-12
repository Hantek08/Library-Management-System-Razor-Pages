﻿using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using MySqlConnector;
using AgilSystemutveckling_Xamarin_Net5.Models;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.TestService.GetService
{
    public class Get
    {

        #region Connection string
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321";
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
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
        }

        // En lista med bara categories
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

            var name = new List<Products>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
        }
        /// <summary>
        /// Gets all products sorted Ö-A.
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
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
        }
        /// <summary>
        /// Gets a specific book.
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetBook()
        {
            string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products
                            WHERE Categories='Book'";

            var name = new List<Products>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
        }
        /// <summary>
        /// Gets a specifik ebook.
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetEbook()
        {
            string? sql = @$"SELECT Title, Authors.AuthorName, SubCategories.SubCategoryName
                            FROM Products
                            WHERE Categories='E-Book'";

            var name = new List<Products>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
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
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<Products>(sql).ToList();
                connection.Close();
            }

            return name;
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
            using (var connection = new MySqlConnection(connString))
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
            using (var connection = new MySqlConnection(connString))
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
                            from Users
                            ORDER BY Username";
            var user = new List<Users>();
            using (var connection = new MySqlConnection(connString))
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
            var sql = @$"Select Username from Users ORDER BY Username desc";
            var user = new List<Users>();
            using (var connection = new MySqlConnection(connString))
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
            string letter = "<first letter of search query here>";
            var sql = @$"SELECT Username 
                               FROM Users 
                               WHERE Username = '{a}%'
                               ORDER BY Username";
            var user = new List<Users>();
            using (var connection = new MySqlConnection(connString))
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
            using (var connection = new MySqlConnection(connString))
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
            using (var connection = new MySqlConnection(connString))
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
            using (var connection = new MySqlConnection(connString))
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
            using (var connection = new MySqlConnection(connString))
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
            using (var connection = new MySqlConnection(connString))
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
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                author = connection.Query<Authors>(sql).ToList();
            }

            return author;
        }

        #endregion

        #region Names related
        // Might be redundant - front-end, please check if these queries are needed.

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
            using (var connection = new MySqlConnection(connString))
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
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                lastName = connection.Query<LastNames>(sql).ToList();
            }

            return lastName;
        }

        #endregion

        #region Loan related
        public static void UserAction (int userID, int productID,int actionID) //vilken siffra ska det stå här för actionID när man lånar bok?
        {
            MySqlConnection connection = new MySqlConnection(connString);

            var cmdText = @$"INSERT INTO History (UserID, ProductID, DateTime, ActionID)
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
