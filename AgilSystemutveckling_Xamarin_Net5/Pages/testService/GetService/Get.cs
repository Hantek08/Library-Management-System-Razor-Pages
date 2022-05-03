using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MySqlConnector;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.testService.Get
{
    public class Get
    {
        
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321";

        public static List<testModel.Product> GetAllProducts()
        {
            var sql = @$"SELECT Title, A.Name, C.Name, S.Name
                        from Products AS P
                        INNER JOIN Authors AS A
                        ON P.Author = A.Id
                        Inner Join Categorys AS C On C.Id = P.Category
                        Inner Join SubCategorys AS S On S.Id = P.SubCategory";
            var name = new List<testModel.Product>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                name = connection.Query<testModel.Product>(sql).ToList();
            }

            return name;
        }


    }
}
