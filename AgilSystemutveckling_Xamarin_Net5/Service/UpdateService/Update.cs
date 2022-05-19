
using AgilSystemutveckling_Xamarin_Net5.Models;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static AgilSystemutveckling_Xamarin_Net5.Constants.Constant;

namespace AgilSystemutveckling_Xamarin_Net5.Service.UpdateService
{
    public class Update
    {
        #region Stock related
        /// <summary>
        /// Updates number of units in stock.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="unitsInStock"></param>
        public static void UpdateUnitsInStock(int id, int unitsInStock)
        {
            //For Admin to update units in Stock
            var cmdText = @$"UPDATE Products 
                            SET UnitsInStock = {unitsInStock}
                            WHERE Id = {id}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    connection.Execute(cmdText);

                    connection.Close();
                }
            }

        }
        #endregion
    }
}
