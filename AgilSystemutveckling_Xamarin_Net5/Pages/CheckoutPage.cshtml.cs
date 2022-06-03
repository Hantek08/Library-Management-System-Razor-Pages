using AgilSystemutveckling_Xamarin_Net5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class CheckoutPageModel : PageModel
    {
        public void OnGet()
        {
        }

        // Delete the items you've chosen from the cart
        public void OnPostDeliteFromCart(int id)
        {
            for (int i = 0; i < Globals.CartList.Count; i++)
            {
                if(id == Globals.CartList[i].Id)
                {
                    Globals.CartList.RemoveAt(i);
                    break;
                }
            }           
        }
        
        //Adds the items you've chosen into History
        public void OnPostCheckOut()
        {
            foreach (var item in Globals.CartList)
            {
                Service.CreateService.Create.AddHistory(Globals.LoggedInUser.Id, item.Id, 1);
                Globals.CartList = new List<Products>();
            }
            TempData["success"] = "Checkout completed"; // Popup
        }
    }
}
