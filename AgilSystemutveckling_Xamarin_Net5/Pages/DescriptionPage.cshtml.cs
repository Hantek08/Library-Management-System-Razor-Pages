using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class DescriptionPageModel : PageModel
    {
        public static TestModels.Product product = new TestModels.Product();
        public static string description;
        public static string title;
        public static string author;
        public static string imgUrl;

        public void OnGet(int id)
        {
            product = TestService.GetService.Get.GetProductById(id);
            description = product.Description;
            title = product.Title;
            author = product.AuthorName;
            imgUrl = product.ImgUrl;
        }

        public void OnPost()
        {
            ViewData["Books"] = description;
            // git test
        }

    }
}
