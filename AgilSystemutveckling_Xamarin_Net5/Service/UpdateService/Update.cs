
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

            CheckStringFormat(user.FirstName, user.LastName);

            int firstNameId = 0;
            int lastNameId = 0;
            int fullNameId = user.FullNameId;

            bool firstNameExists = false;
            bool lastNameExists = false;

            var firstNames = Service.GetService.Get.GetAllFirstNames();
            if (firstNames == null) { throw new NullReferenceException(); }

            foreach (var item in firstNames)
            {
                if (item != null && user.FirstName == item.FirstName)
                {
                    firstNameId = item.Id;
                    firstNameExists = true;
                    break;
                }
            }

            if (firstNameExists == false)
            {

                var sql = @$"INSERT INTO FirstNames (FirstName)
                                    VALUES ('{user.FirstName}')";


                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        connection.Execute(sql);

                    connection.Close();
                }

                var sql2 = @$"SELECT Id
                                    FROM FirstNames
                                    WHERE FirstName = '{user.FirstName}'";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        firstNameId = connection.QuerySingle<int>(sql2);

                    connection.Close();
                }
            }

            var lastNames = Service.GetService.Get.GetAllLastNames();
            if (lastNames == null) { throw new NullReferenceException(); }

            foreach (var item in lastNames)
            {
                if (item == null) { throw new NullReferenceException(); }

                if (user.LastName == item.LastName)
                {
                    lastNameId = item.Id;
                    lastNameExists = true;
                    break;
                }
            }


            if (lastNameExists == false)
            {
                var sql = @$"INSERT INTO LastNames (LastName) 
                                    VALUES ('{user.LastName}')";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        connection.Execute(sql);

                    connection.Close();
                }

                var sql2 = @$"SELECT Id
                                    FROM LastNames
                                    WHERE LastName = '{user.LastName}'";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                        lastNameId = connection.QuerySingle<int>(sql2);

                    connection.Close();
                }
            }

            var sqlFN = @$"UPDATE FullNames 
                           SET FirstNameId = {firstNameId}, LastNameId = {lastNameId})
                           WHERE Id = {fullNameId}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    connection.Execute(sqlFN);

                connection.Close();
            }

            CheckStringFormat(user.Username, user.Password, user.Address);

            var sqlMain = @$"UPDATE Users 
                            SET FullNameId = {fullNameId}, Username = '{user.Username}', Password = '{user.Password}', 
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
