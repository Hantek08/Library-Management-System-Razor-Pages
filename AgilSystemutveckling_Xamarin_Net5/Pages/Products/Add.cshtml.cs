using AgilSystemutveckling_Xamarin_Net5.Data;
using AgilSystemutveckling_Xamarin_Net5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Models.Products products { get; set; }
        private readonly LibraryDbContext _dbContext;
        public AddModel(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            products = new Models.Products();  
        }

        public IActionResult OnPost()
        {

            
        return RedirectToPage("Index");
        }
    }
}
