using AgilSystemutveckling_Xamarin_Net5.Data;
using AgilSystemutveckling_Xamarin_Net5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Models.Products products { get; set; }
        private readonly LibraryDbContext _dbContext;
        public EditModel(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(string id)
        {
            products = _dbContext.Products.Find(id);
        }

        public IActionResult OnPost()
        {
        }
    }
}
