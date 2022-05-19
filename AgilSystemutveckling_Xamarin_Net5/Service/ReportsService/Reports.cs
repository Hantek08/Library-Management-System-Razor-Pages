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

        public static List<History?> MostPopularBook()
        {
            string? sql = @$"SELECT Title,Count(Products.Id) as '# lent'
			FROM History
			INNER JOIN Actions on ActionId = Actions.Id
			INNER JOIN Products on ProductId =  Products.Id
			group by Title 
            order by Count(Products.Id) desc limit 3
            ";
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var history = connection.Query<History?>(sql).ToList();

                    connection.Close();

                    return history;
                }
            }

            return null;
        
        }


    }
}
