using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Models;
using AgilSystemutveckling_Xamarin_Net5.Service.GetService;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class SearchPageModel : PageModel
    {
        public static List<Products> BookName { get; set; }

        public void OnGet()
        {
            BookName = Get.GetAllProducts();
        }

        public void OnPost()
        {
            ViewData["Books"] = BookName;
        }
    }


}
