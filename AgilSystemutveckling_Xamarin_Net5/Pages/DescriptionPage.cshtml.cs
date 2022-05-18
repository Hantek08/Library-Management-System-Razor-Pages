using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class DescriptionPageModel : PageModel
    {
        public static Models.Products product = new Models.Products();
        public static string description;
        public static string title;
        public static string author;
        public static string imgUrl;

        public void OnGet(int id)
        {
            product = Service.GetService.Get.GetProductById(id);
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
        public void OnPostAddToCart()
        {

            Globals.CartList.Add(product);
            Thread.Sleep(3000);
            //return RedirectToPage("/DescriptionPage", new { id = product.Id });
        }

    }
}
