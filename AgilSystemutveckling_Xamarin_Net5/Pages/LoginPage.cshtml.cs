using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class LoginPageModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {

        }


        public IActionResult OnPost()
        {

            List<TestModels.User> users = TestService.GetService.Get.GetAllUsers();

            foreach (var item in users)
            {
                if (Username.Equals(item.Username) && Password.Equals(item.Password))
                {
                    /*HttpContext.Session.SetString("username", Username);
                    HttpContext.Session.SetString(Username, username);*/
                    Globals.LoggedInUser = item;
                    return RedirectToPage("/SearchPage");
                }
            }
            return Page();
        }
    }
}
