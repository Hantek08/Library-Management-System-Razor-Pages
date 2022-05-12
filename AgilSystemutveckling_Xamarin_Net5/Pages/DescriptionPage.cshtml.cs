using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class DescriptionPageModel : PageModel
    {
        public static List<TestModels.Product> products = new List<TestModels.Product>();
        public static TestModels.Product product = new TestModels.Product();
        public static string description;
        public static string title;
        public static string author;

        public void OnGet()
        {
            products = TestService.GetService.Get.GetAllProducts();
            product = products[0];
            description = product.Description;
            title = product.Title;
            author = product.AuthorName;
        }

        public void OnPost()
        {
            ViewData["Books"] = description;
            // git test
        }

    }
}
