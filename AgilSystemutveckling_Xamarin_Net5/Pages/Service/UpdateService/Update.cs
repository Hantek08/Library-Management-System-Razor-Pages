
using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using MySqlConnector;
using AgilSystemutveckling_Xamarin_Net5.Models;




namespace AgilSystemutveckling_Xamarin_Net5.Pages.UpdateService
{ 

public class Update
{
    #region Connection string
    static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321";
    #endregion

    public static void UpdateUnitsInStock(int id, int unitsInStock)
    {//For Admin to update units in Stock.

        MySqlConnection connection = new MySqlConnection(connString);

        var cmdText = @$"Update Products Set UnitsInStock
                                = @UnitsInStock
                      
                        Where Id = @Id";


        var cmd = new MySqlCommand(cmdText, connection);



        cmd.Parameters.AddWithValue($"@UnitsInStock", unitsInStock);
        cmd.Parameters.AddWithValue($"Id", id);



        connection.Open();
        int r = cmd.ExecuteNonQuery();
        connection.Close();

    }

}

}
