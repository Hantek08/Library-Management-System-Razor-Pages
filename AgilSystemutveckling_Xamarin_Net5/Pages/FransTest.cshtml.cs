using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Service.GetService;
using AgilSystemutveckling_Xamarin_Net5.Models;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class FransTestModel : PageModel
    {

        public static List<History> histories = new List<History>();

        public void OnGet()
        {
            Service.CreateService.Create.AddHistory(8, 16, 1);
            histories = Service.GetService.Get.ActiveLoans(8);
        }
    }
}
