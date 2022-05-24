using AgilSystemutveckling_Xamarin_Net5.Models;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static AgilSystemutveckling_Xamarin_Net5.Constants.Constant;
using AgilSystemutveckling_Xamarin_Net5.Service.GetService;



namespace AgilSystemutveckling_Xamarin_Net5.Service.ReportsService
{
    public class Reports
    {

        /// <summary>
        /// Gets books sorted by number of loans.
        /// </summary>
        /// <returns></returns>
        public static List<Products?> MostPopularProducts(int limitBy)
        {
            string? sql = @$"SELECT Products.Id, Products.Title, Products.Description,
                            Authors.AuthorName, Categories.CategoryName, SubCategories.SubCategoryName,
                            Products.UnitsInStock, Products.InStock, Products.ImgUrl
                            FROM History
                            INNER JOIN Products on ProductId =  Products.Id
                            INNER JOIN Authors on AuthorId = Authors.Id
                            INNER JOIN Categories on Products.CategoryId = Categories.Id
                            INNER JOIN SubCategories on Products.SubCategoryId = SubCategories.Id
                            GROUP BY Products.Title
                            order by count(ProductId) desc
                            limit {limitBy}";

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

        public static List<FullNames?> MostActiveUser(int limitBy)
        {
            string? sql = @$"SELECT FirstNames.FirstName, LastNames.LastName
                            FROM History
                            inner join Users on History.UserId = Users.Id
                            inner join FullNames on Users.FullNameId = FullNames.Id
                            inner join FirstNames on FullNames.FirstNameId = FirstNames.Id
                            inner join LastNames on FullNames.LastNameId = LastNames.Id
                            group by FullNameId
                            order by count(UserId) desc
                            limit {limitBy}";
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var history = connection.Query<FullNames?>(sql).ToList();

                    connection.Close();

                    return history;
                }
            }

            return null;

        }


    }
}
