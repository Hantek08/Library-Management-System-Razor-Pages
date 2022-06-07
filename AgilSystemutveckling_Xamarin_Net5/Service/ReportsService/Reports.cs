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

        /// <summary>
        /// Gets the most active users.
        /// </summary>
        /// <param name="limitBy"></param>
        /// <returns></returns>
        public static List<FullNames?>? MostActiveUsers(int limitBy)
        {
            string? sql = @$"SELECT FirstNames.FirstName, LastNames.LastName
                            FROM History
                            INNER JOIN Users ON History.UserId = Users.Id
                            INNER JOIN FullNames ON Users.FullNameId = FullNames.Id
                            INNER JOIN FirstNames ON FullNames.FirstNameId = FirstNames.Id
                            INNER JOIN LastNames ON FullNames.LastNameId = LastNames.Id
                            GROUP BY FullNameId
                            ORDER BY COUNT(UserId) DESC
                            LIMIT {limitBy}";
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var fullName = connection.Query<FullNames?>(sql).ToList();

                    connection.Close();

                    return fullName;
                }
            }

            return null;
        }
    }
}
