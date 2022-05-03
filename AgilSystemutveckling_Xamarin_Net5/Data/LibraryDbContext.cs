using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AgilSystemutveckling_Xamarin_Net5.Models;
using Microsoft.AspNetCore.Hosting.Server;
using MySqlConnector;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AgilSystemutveckling_Xamarin_Net5.Data
{
    public class LibraryDbContext : DbContext
    {

        public readonly IConfiguration Configuration;

        public LibraryDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // Configure database to be used (MySQL).

      
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321;";
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        // Create following table(s).
        public DbSet<Names> Names { get; set; }
        public DbSet<SurNames> SurNames { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<SubCategories> SubCategories { get; set; }
    }
}
