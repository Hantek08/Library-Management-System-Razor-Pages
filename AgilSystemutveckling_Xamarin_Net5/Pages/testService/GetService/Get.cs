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
            }

            return name;
        }

        public static List<TestModels.User> GetAllUsers()
        {
            var sql = @$"Select * from User";
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
