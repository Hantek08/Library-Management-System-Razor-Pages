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

        public void OnPostCheckOut()
        {
            foreach (var item in Globals.CartList)
            {
                Service.CreateService.Create.AddLoan(Globals.LoggedInUser.Id, item.Id, 1);
                Globals.CartList = new List<Products>();
            }
        }
    }
}
