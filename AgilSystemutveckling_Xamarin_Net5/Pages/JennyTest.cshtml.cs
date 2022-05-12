using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Models;
using AgilSystemutveckling_Xamarin_Net5.Pages.GetService;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class JennyTestModel : PageModel
    {
        public void OnGet()
        {
            History history = new History();
            history.ActionId = 1;
            history.ProductId = 1;
            history.Time = DateTime.Now;
            history.UserId = 1;

            Get.UserAction(1,1,1);
            

        }
    }
}
