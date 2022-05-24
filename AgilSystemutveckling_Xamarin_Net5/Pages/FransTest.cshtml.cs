using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Service.GetService;
using AgilSystemutveckling_Xamarin_Net5.Models;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class FransTestModel : PageModel
    {

        List<Users> users = new List<Users>();

        public void OnGet()
        {
            users = Service.GetService.Get.GetAllUsers();

            users[0].Address = "Regnbågen 2";

            Service.UpdateService.Update.User(users[0]);

        }
    }
}
