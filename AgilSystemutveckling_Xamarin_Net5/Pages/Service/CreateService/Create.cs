using AgilSystemutveckling_Xamarin_Net5.Models;
using Dapper;
using MySqlConnector;
using System.Xml;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.Service.CreateService
{
    public class Create
    {
        static string connString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321; AllowUserVariables=True;";

        #region User related
        public static void AddUser(Users user)
        {
            int fullNameId = 0;
            int firstNameId = 0;
            int lastNameId = 0;

            bool firstNameExists = false;
            bool lastNameExists = false;
            
            List<FirstNames> firstNames = GetService.Get.GetAllFirstNames();
            foreach (var item in firstNames)
            {
                if (user.FirstName == item.FirstName)
                {
                    firstNameId = item.Id;
                    firstNameExists = true;
                    break;
                }
            }

            if (firstNameExists == false) 
            {
                var sql = @$"insert into FirstNames (FirstName) 
                        values ('{user.FirstName}')";
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                }

                var sql2 = @$"SELECT Id
                                FROM FirstNames
                                where FirstName = '{user.FirstName}'";

                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    firstNameId = connection.QuerySingle<int>(sql2);
                }
            }

            List<LastNames> lastNames = GetService.Get.GetAllLastNames();
            foreach (var item in lastNames)
            {
                if (user.LastName == item.LastName)
                {
                    lastNameId = item.Id;
                    lastNameExists = true;
                    break;
                }
            }

            if (lastNameExists == false)
            {
                var sql = @$"insert into LastNames (LastName) 
                        values ('{user.LastName}')";
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                }

                var sql2 = @$"SELECT Id
                                FROM LastNames
                                where LastName = '{user.LastName}'";

                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    lastNameId = connection.QuerySingle<int>(sql2);
                }
            }

            var sqlFN = @$"insert into FullNames (FirstNameId, LastNameId) 
                        values ({firstNameId}, {lastNameId})";
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sqlFN);
            }

            var sqlFN2 = @$"SELECT Id
                            FROM FullNames
                            where LastNameId = {lastNameId} and FirstNameId = {firstNameId}";

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                fullNameId = connection.QuerySingle<int>(sqlFN2);
            }

            var sqlMain = @$"insert into Users (FullNameId, Username, Password, AccessId,
                                                    Address, Blocked) 
                        values ({fullNameId}, '{user.Username}', '{user.Password}', {user.Level}, '{user.Address}',
                                {user.Blocked})";
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sqlMain);
            }

        }
        #endregion

        #region Author related
        public static Models.Authors AddAuthor(Models.Authors author)
        {
            // author.Id = "<input from user>" - if auto_increment cant be used.

            MySqlConnection connection = new MySqlConnection(connString);


            var cmdText = @$"INSERT INTO Authors (AuthorName)
                                VALUES (@AuthorName)";


            var cmd = new MySqlCommand(cmdText, connection);
            // cmd.Parameters.AddWithValue($"@Id", author.Id); - same as above comment.
            cmd.Parameters.AddWithValue($"@AuthorName", author.AuthorName);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return author;
        }
        #endregion

        public static void AddProduct(Products product)
        {
            static List<Authors> GetAllAuthors()
            {
                
                var sql = @$"Select Id, AuthorName 
                                From Authors";
                var author = new List<Models.Authors>();
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    author = connection.Query<Models.Authors>(sql).ToList();
                }

                return author;
            }

            static List<Models.Categories> GetAllCategories()
            {
                var sql = @$"Select Id, CategoryName 
                                From Categories";
                var categories = new List<Models.Categories>();
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    categories = connection.Query<Models.Categories>(sql).ToList();
                }

                return categories;
            }

            static List<Models.SubCategories> GetAllSubCategories()
            {
                var sql = @$"Select Id, SubCategoryName 
                                From SubCategories";
                var subCategories = new List<Models.SubCategories>();
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    subCategories = connection.Query<Models.SubCategories>(sql).ToList();
                }

                return subCategories;
            }

            int AuthorId = 0;
            int CategoryId = 0;
            int SubCategoryId = 0;

            bool AuthorExists = false;
            bool CategoryExists = false;
            bool SubCategoryExists = false;

            List<Models.Authors> authors = GetAllAuthors();
            foreach (var author in authors) 
            {
                if (author.AuthorName == product.AuthorName) 
                {
                    AuthorId = author.Id;
                    AuthorExists = true;
                    break;
                }
            }

            if (AuthorExists == false)
            {
                var sql = @$"insert into Authors (AuthorName) 
                        values ('{product.AuthorName}')";
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                }

                var sql2 = @$"SELECT Id
                                FROM Authors
                                where AuthorName = '{product.AuthorName}'";

                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    AuthorId = connection.QuerySingle<int>(sql2);
                }
            }

            List<Models.Categories> categories = GetAllCategories();
            foreach (var category in categories)
            {
                if (category.CategoryName == product.CategoryName)
                {
                    CategoryId = category.Id;
                    CategoryExists = true;
                    break;
                }
            }

            if (CategoryExists == false)
            {
                var sql = @$"insert into Categories (CategoryName) 
                        values ('{product.CategoryName}')";
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                }

                var sql2 = @$"SELECT Id
                                FROM Categories
                                where CategoryName = '{product.CategoryName}'";

                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    CategoryId = connection.QuerySingle<int>(sql2);
                }
            }

            List<Models.SubCategories> subCategories = GetAllSubCategories();
            foreach (var subCategory in subCategories)
            {
                if (subCategory.SubCategoryName == product.SubCategoryName)
                {
                    SubCategoryId = subCategory.Id;
                    SubCategoryExists = true;
                    break;
                }
            }

            if (SubCategoryExists == false)
            {
                var sql = @$"insert into SubCategories (SubCategoryName) 
                        values ('{product.SubCategoryName}')";
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                }

                var sql2 = @$"SELECT Id
                                FROM SubCategories
                                where SubCategoryName = '{product.SubCategoryName}'";

                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    SubCategoryId = connection.QuerySingle<int>(sql2);
                }
            }

            var sqlMain = @$"insert into Products (Title, Description, AuthorId, CategoryId,
                                                    SubCategoryId, UnitsInStock, ImgUrl) 
                        values ('{product.Title}', '{product.Description}', {AuthorId}, {CategoryId}, {SubCategoryId},
                                {product.UnitsInStock}, '{product.ImgUrl}')";
            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sqlMain);
            }
        }

        #region Category related
        public static Models.Categories AddCategory(Categories category)
        {

            MySqlConnection connection = new MySqlConnection(connString);

            var cmdText = @$"INSERT INTO Categories (CategoryName)
                                VALUES (@CategoryName)";

            var cmd = new MySqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue($"@CategoryName", category.CategoryName);

            connection.Open();
            int r = cmd.ExecuteNonQuery();
            connection.Close();


            return category;
        }
        #endregion

        #region Subcategory related
        public static Models.SubCategories AddSubCategory(Models.SubCategories subcategory)
        {

            MySqlConnection connection = new MySqlConnection(connString);

            var cmdText = @$"INSERT INTO SubCategory (SubCategoryName)
                                VALUES (@SubCategoryName)";

            var cmd = new MySqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue($"@SubCategoryName", subcategory.SubCategoryName);

            connection.Open();
            int r = cmd.ExecuteNonQuery();
            connection.Close();

            return subcategory;
        }
        #endregion

        

    }
}
