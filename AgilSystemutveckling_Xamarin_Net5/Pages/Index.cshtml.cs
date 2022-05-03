using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Models;
using AgilSystemutveckling_Xamarin_Net5.Data;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly LibraryDbContext _dbContext;
        public IndexModel(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            
        }

        
    }
}