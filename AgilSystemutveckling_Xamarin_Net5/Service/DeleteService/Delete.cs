using Dapper;
using MySqlConnector;

using static AgilSystemutveckling_Xamarin_Net5.Constants.Constant;

namespace AgilSystemutveckling_Xamarin_Net5.Service.DeleteService
{
    public static class Delete
    {


        #region Author related
        public static void DeleteAuthor(int id)
        {
            var sql = @$"DELETE FROM Author
                                WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Execute(sql, new { id = id });

                    connection.Close();
                }
            }
        }
            #endregion


            #region Product related


            /// <summary>
            /// Deletes a product from the database.
            /// </summary>
            /// <param name="id"></param>
            public static void DeleteProduct(int id)
            { //Deletes products by id! User enters id of the product that should be deleted.

                var sql = @$"DELETE FROM Products
                                WHERE id = {id}";

                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Execute(sql, new { id = id });

                        connection.Close();
                    }
                }
            }

            #endregion


            #region User related
            //funkar ej

            //public static void DeleteUser(int id)
            //{
            //    var sql = @$"DELETE FROM User
            //                       WHERE id = @id";

            //    using (MySqlConnection connection = new MySqlConnection(connString))
            //    {
            //        connection.Open();

            //        if ((connection != null) || (connection.State == System.Data.ConnectionState.Open))
            //        {
            //            try
            //            {
            //                connection.Execute(sql, new { id = id });
            //            }
            //            catch (Exception e)
            //            {
            //                Console.WriteLine(e.Message);
            //            }
            //        }
            //        else
            //        {
            //            // display to user that connection could not be made
            //        }
            //    }
            //}
            #endregion
        }
    }

