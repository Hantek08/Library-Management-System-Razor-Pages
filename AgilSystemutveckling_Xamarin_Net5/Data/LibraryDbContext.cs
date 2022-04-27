using Microsoft.EntityFrameworkCore;
using AgilSystemutveckling_Xamarin_Net5.DatabaseModel;

namespace AgilSystemutveckling_Xamarin_Net5.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base (options) 
        { 

        }
        public DbSet<Names> Names { get; set; }
        public DbSet<Surnames> Surnames { get; set; }
    }
}
