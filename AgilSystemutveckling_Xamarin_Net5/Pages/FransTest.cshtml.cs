using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class FransTestModel : PageModel
    {
        public static Models.Products products;

        public void OnGet()
        {
            products = GetService.Get.GetProductById(1);
        }
    }
}