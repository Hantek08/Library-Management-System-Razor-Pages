using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class FransTestModel : PageModel
    {
        public void OnGet()
        {
            Models.Users user = new Models.Users()
            {
                Username = "xamarin",
                Password = "123",
                FirstName = "Göran",
                LastName = "Holm",
                Level = 1,
                Address = "Blombacken 21",
                Blocked = false
            };

            Service.CreateService.Create.AddUser(user);
        }
    }
}
