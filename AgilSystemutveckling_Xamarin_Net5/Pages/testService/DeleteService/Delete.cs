using Dapper;
using MySqlConnector;
using System.Data.SqlClient;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.testService
{
    public class Delete
    {
        #region Connection string
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321";
        #endregion
        public static void DeleteProduct(int id)
        {
            var sql = $"DELETE FROM Product WHERE id = @id";

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                try
                {
                    connection.Execute(sql, new { id = id });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public static void DeleteUser(int id)
        {
            var sql = $"DELETE FROM User WHERE id = @id";

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                try
                {
                    connection.Execute(sql, new { id = id });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}

