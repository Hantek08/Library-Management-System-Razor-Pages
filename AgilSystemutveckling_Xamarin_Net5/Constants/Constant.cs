﻿namespace AgilSystemutveckling_Xamarin_Net5.Constants
{
    public class Constant
    {
        private const string? _connectionString = $"Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321;";
        public static string? connectionString  => _connectionString;
    }
}
