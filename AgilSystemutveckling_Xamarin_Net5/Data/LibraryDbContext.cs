using Microsoft.EntityFrameworkCore;
using AgilSystemutveckling_Xamarin_Net5.Models;
using Microsoft.AspNetCore.Hosting.Server;
using MySqlConnector;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AgilSystemutveckling_Xamarin_Net5.Data
{

    
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base (options)
        {
           
        }
    }
}
