using System;

public class Class1
{
	public Class1()
	{
		static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321;";

        public static List<Models.Produkt> MenuChoice(int choice)
        {
            var sql = @$"SELECT title FROM Products";
            var namn = new List<testModel.Product>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                namn = connection.QuerySingle<testModel.Produkt>(sql).ToList();
            }

            return namn;
        }
    }
}
