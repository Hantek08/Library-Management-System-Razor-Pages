using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class SearchPageModel : PageModel
    {
        public static List<testModel.Product> BookName;

        public void OnGet()
        {
            BookName = testService.getProducts.products();
        }

        public void OnPost()
        {
            ViewData["Books"] = BookName;
        }
    }


}
