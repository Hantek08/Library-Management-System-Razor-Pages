using AgilSystemutveckling_Xamarin_Net5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class ActiveLoansPageModel : PageModel
    {
        public List<History?> LoandProductList = new List<History?>();

        // Get a list of loans made by one user.
        // Uses ID  to get right user
        public void OnGet()
        {
            LoandProductList = Service.GetService.Get.ActiveLoans(Globals.LoggedInUser.Id);
        }


        // Adds the products you've chosen to History based on user íd and item id
        // Also set the action to "loand"
        public void OnPostReturnProduct(int itemid)
        {
            Service.CreateService.Create.AddHistory(Globals.LoggedInUser.Id, itemid, 2);
            
            TempData["success"] = "Product has been returned"; // Popup
        }
    }
}
