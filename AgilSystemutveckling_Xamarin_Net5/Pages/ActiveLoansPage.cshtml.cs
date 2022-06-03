using AgilSystemutveckling_Xamarin_Net5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class ActiveLoansPageModel : PageModel
    {
        public List<History?> LoandProductList = new List<History?>();

        public void OnGet()
        {
            LoandProductList = Service.GetService.Get.ActiveLoans(Globals.LoggedInUser.Id);
        }
        public void OnPostReturnProduct(int itemid)
        {
            Service.CreateService.Create.AddHistory(Globals.LoggedInUser.Id, itemid, 2);
            
            TempData["success"] = "Product has been returned";
        }
    }
}
