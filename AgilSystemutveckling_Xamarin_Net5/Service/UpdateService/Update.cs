
using AgilSystemutveckling_Xamarin_Net5.Models;
using Dapper;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static AgilSystemutveckling_Xamarin_Net5.Constants.Constant;
using static AgilSystemutveckling_Xamarin_Net5.Methods.Methods;


namespace AgilSystemutveckling_Xamarin_Net5.Service.UpdateService
{
    public static class Update
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

        #region user related
        public static void User(Users? user)
        {
            CheckIfObjectIsNull(user);

            CheckStringFormat(user.Username, user.Password, user.Address);

            var sqlMain = @$"UPDATE Users 
                            SET Username = '{user.Username}', Password = '{user.Password}', 
                            AccessId = {user.Level}, Address = '{user.Address}', Blocked = {user.Blocked}
                            WHERE Id = {user.Id}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    try { connection.Execute(sqlMain); }
                    catch (Exception e) { throw new Exception("Could not update user.", e); }

                connection.Close();
            }
        }
        #endregion
    }
}
