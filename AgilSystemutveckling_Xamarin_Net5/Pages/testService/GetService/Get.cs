using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using MySqlConnector;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.TestService.GetService
{
    public class Get
    {
        
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321";

        public static List<TestModels.Product> GetAllProducts()
        {
            var sql = @$"SELECT Title, Description, Authors.AuthorName, 
                        Categories.CategoryName, SubCategories.SubCategoryName
                        from Products
                        INNER JOIN Authors ON Products.AuthorId = Authors.Id
                        INNER JOIN Categories ON Products.CategoryId = Categories.Id
                        INNER JOIN SubCategories ON Products.SubCategoryId = SubCategories.Id";
            var name = new List<TestModels.Product>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<TestModels.Product>(sql).ToList();
            }

            return name;
        }

        public static List<TestModels.User> GetAllUsers()
        {
            var sql = @$"Select Users.Id, Users.Username, Users.Password, Users.Address, Users.Blocked, 
                        FirstNames.FirstName, LastNames.LastName, Access.Level
                        from Users
                        inner join FullNames on Users.FullNameId = FullNames.Id
                        inner join FirstNames on FullNames.FirstNameId = FirstNames.Id
                        inner join LastNames on FullNames.LastNameId = LastNames.Id
                        inner join Access on Users.AccessId = Access.Id";
            var user = new List<TestModels.User>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                user = connection.Query<TestModels.User>(sql).ToList();
            }

            return user;
        }



    }
}
