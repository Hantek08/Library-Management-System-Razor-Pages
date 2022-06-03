
using AgilSystemutveckling_Xamarin_Net5.Models;
using Dapper;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static AgilSystemutveckling_Xamarin_Net5.Constants.Constant;
using static AgilSystemutveckling_Xamarin_Net5.Methods.Methods;
using static AgilSystemutveckling_Xamarin_Net5.Service.GetService.Get;


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
        {// This method updates user 
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

        public static void Product(Products? product)
        {
            CheckIfObjectIsNull(product);

            CheckStringFormat(product.AuthorName, product.CategoryName, product.SubCategoryName, product.Description, product.Title);

            int categoryId = 0;
            int subCategoryId = 0;
            List<Categories?> categories = GetAllCategories();
            List<SubCategories?> subCategories = GetAllSubCategories();

            foreach(var category in categories)
            {
                if (product.CategoryName == category.CategoryName)
                    categoryId = category.Id;
            }
            foreach(var subCategory in subCategories)
            {
                if (product.SubCategoryName == subCategory.SubCategoryName)
                    subCategoryId = subCategory.Id;
            }

            var sql1 = @$"UPDATE Products 
                                    SET Description = '{product.Description}', Title = '{product.Title}', 
                                        UnitsInStock = {product.UnitsInStock}, Active = {product.Active}, 
                                        ImgUrl = '{product.ImgUrl}', CategoryId = {categoryId}, SubCategoryId = {subCategoryId}
                                    WHERE Id = {product.Id}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                    try { connection.Execute(sql1); }
                    catch (Exception e) { throw new Exception("Could not update product.", e); }

                connection.Close();
            }
        }
    }
        #endregion
}
