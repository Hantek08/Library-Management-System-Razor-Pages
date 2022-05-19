using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Service.GetService;
using AgilSystemutveckling_Xamarin_Net5.Models;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class FransTestModel : PageModel
    {
        public static List<Products> histories = new List<Products>();

        public void OnGet()
        {
            histories = Service.GetService.Get.MostPopularProducts(1);
        }
    }
}
