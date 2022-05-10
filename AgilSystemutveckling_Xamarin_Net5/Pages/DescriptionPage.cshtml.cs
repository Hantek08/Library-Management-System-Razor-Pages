using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class DescriptionPageModel : PageModel
    {
        public static TestModels.Product products;
        public static string description;
        public static string title;
        public static string author;

        public void OnGet()
        {
            products = TestService.GetService.Get.GetProductById(2);
            description = products.Description;
            title = products.Title;
            author = products.AuthorName;
        }

        public void OnPost()
        {
            ViewData["Books"] = description;
            // git test
        }

    }
}
