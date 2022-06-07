
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
        //For Admin to update units in Stock

        #region Stock related
        /// <summary>
        /// Updates number of units in stock by product id and specified number of products.
        /// Method is purposely implemented for Administrators.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="unitsInStock"></param>
        public static void UpdateUnitsInStock(int id, int unitsInStock)
        {

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

        #region User related
        /// <summary>
        /// Updates a specified User.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static void User(Users? user)
        {
            if (user is null) { throw new ArgumentNullException(nameof(user)); }

            CheckStringFormat(user.Username, user.Password, user.Address);

            var sqlMain = @$"UPDATE Users 
                                    SET Username = '{user.Username}', Password = '{user.Password}', 
                                        AccessId = {user.Level}, Address = '{user.Address}', 
                                        Blocked = {user.Blocked}
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
        /// <summary>
        /// Updates a specific Product.
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public static void Product(Products? product)
        {
            if (product is null) { throw new ArgumentNullException(nameof(product)); }

            CheckStringFormat(product.AuthorName, product.CategoryName, product.SubCategoryName,
                              product.Description, product.Title, product.ImgUrl);

            int categoryId = 0;
            int subCategoryId = 0;

            List<Categories?>? categories = GetAllCategories();
            if (categories is null) { throw new NullReferenceException(nameof(categories)); }

            List<SubCategories?>? subCategories = GetAllSubCategories();
            if (subCategories is null) { throw new NullReferenceException(nameof(subCategories)); }

            foreach (var category in categories)
            {
                if (category is null) { throw new NullReferenceException(nameof(category)); }
                if (product.CategoryName == category.CategoryName)
                    categoryId = category.Id;
            }
            foreach (var subCategory in subCategories)
            {
                if (subCategory is null) { throw new NullReferenceException(nameof(subCategory)); }
                if (product.SubCategoryName == subCategory.SubCategoryName)
                    subCategoryId = subCategory.Id;
            }

            var sql1 = @$"UPDATE Products 
                                SET Description = '{product.Description}', Title = '{product.Title}', 
                                    UnitsInStock = {product.UnitsInStock}, Active = {product.Active}, 
                                    ImgUrl = '{product.ImgUrl}', CategoryId = {categoryId}, 
                                    SubCategoryId = {subCategoryId}
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
