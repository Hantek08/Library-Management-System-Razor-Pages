using AgilSystemutveckling_Xamarin_Net5.Models;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using static AgilSystemutveckling_Xamarin_Net5.Constants.Constant;
using AgilSystemutveckling_Xamarin_Net5.Service.GetService;



namespace AgilSystemutveckling_Xamarin_Net5.Service.ReportsService
{
    public static class Reports
    {
        #region Product related
        /// <summary>
        /// Gets books sorted by number of loans.
        /// </summary>
        /// <returns>A list of the most popular products within a specified limit of products.</returns>
        public static List<Products?>? MostPopularProducts(int limitBy)
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM History
                            INNER JOIN Products ON ProductId =  Products.Id
                            INNER JOIN Authors ON AuthorId = Authors.Id
                            INNER JOIN Categories ON Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories ON Products.SubCategoryId = SubCategories.Id
                            GROUP BY Products.Title
                            ORDER BY COUNT(ProductId) DESC
                            LIMIT {limitBy}";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var histories = connection.Query<Products?>(sql).ToList();
                    connection.Close();

                    return histories;
                }
            }

            return null;
        }
        #endregion
    }
}
