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
            var sql = @$"SELECT Title, A.Name, C.Name, S.Name
                        from Products AS P
                        INNER JOIN Authors AS A
                        ON P.Author = A.Id
                        Inner Join Categorys AS C On C.Id = P.Category
                        Inner Join SubCategorys AS S On S.Id = P.SubCategory";
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
            var sql = @$"Select Username, Password from Users";
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
