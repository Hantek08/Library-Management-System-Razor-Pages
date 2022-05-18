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

        }
    }
}
