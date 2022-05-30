using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.testPages
{
    public class FransModel : PageModel
    {
        public static List<Models.History> history = new List<Models.History>();
        public void OnGet()
        {
            history = Service.GetService.Get.ActiveLoans(8);
        }
    }
}
