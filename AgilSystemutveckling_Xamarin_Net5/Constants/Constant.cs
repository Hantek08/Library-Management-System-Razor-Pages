
using System.Configuration;

namespace AgilSystemutveckling_Xamarin_Net5.Constants
{
    abstract class Constant
    {

        private string? _connectionString  = ConfigurationManager["LibraryDBConnection"].connectionString;
        public static string? ConnectionString  => _connectionString;
    }
}
