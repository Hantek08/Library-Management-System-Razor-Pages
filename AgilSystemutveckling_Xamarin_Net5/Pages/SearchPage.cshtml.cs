using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class SearchPageModel : PageModel
    {
        public List<Models.Books> BookName;

        public void OnGet()
        {
            BookName = new List<Models.Books>() 
            {
                new Models.Books() { Title = "Harry Potter"}
            };

        }
    }
}
