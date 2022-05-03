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
            var sql = @$"SELECT Title, FROM Products";
            var namn = new List<testModel.Product>();
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                namn = connection.Query<testModel.Product>(sql).ToList();
            }

            return namn;
        }


    }
}
