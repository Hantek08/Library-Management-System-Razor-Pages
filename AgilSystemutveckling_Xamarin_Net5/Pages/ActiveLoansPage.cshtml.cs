using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class ActiveLoansPageModel : PageModel
    {
        var t = Service.GetService.Get.ActiveLoans(Globals.LoggedInUser.Id);
        public void OnGet()
        {
        }
        public void OnPostReturnProduct()
        {

        }
    }
}
