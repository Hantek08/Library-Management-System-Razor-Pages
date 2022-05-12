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


        #region Author related
        public static void DeleteAuthor(int id)
        {
            var sql = @$"DELETE FROM Author
                                WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(connString))
            {
                connection.Open();

                if ((connection != null) || (connection.State == System.Data.ConnectionState.Open))
                {
                    try
                    {
                        connection.Execute(sql, new { id = id });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    // display to user that connection could not be made
                }
            }
        }
        #endregion


        #region Product related
        public static void DeleteProduct(int id)
        {
            var sql = @$"DELETE FROM Product
                                WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(connString))
            {
                connection.Open();

                if ((connection != null) || (connection.State == System.Data.ConnectionState.Open))
                {
                    try
                    {
                        connection.Execute(sql, new { id = id });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    // display to user that connection could not be made
                }
            }
        }

        #endregion


        #region User related
        public static void DeleteUser(int id)
        {
            var sql = @$"DELETE FROM User
                               WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(connString))
            {
                connection.Open();

                if ((connection != null) || (connection.State == System.Data.ConnectionState.Open))
                {
                    try
                    {
                        connection.Execute(sql, new { id = id });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    // display to user that connection could not be made
                }
            }
        }
        #endregion


    }
}

