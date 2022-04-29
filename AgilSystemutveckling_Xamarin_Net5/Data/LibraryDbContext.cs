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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        // Create following table(s).
        public DbSet<Names> Names { get; set; }
        public DbSet<SurNames> SurNames { get; set; }
    }
}
