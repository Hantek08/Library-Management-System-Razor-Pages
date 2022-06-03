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
        [BindProperty]
        public static string LoginError { get; set; }

        public void OnGet()
        {

        }


        public IActionResult OnPost()
        {

            List<Models.Users> users = Service.GetService.Get.GetAllUsers();

            foreach (var item in users)
            {
                if (Username.Equals(item.Username) && Password.Equals(item.Password))
                {
                    /*HttpContext.Session.SetString("username", Username);
                    HttpContext.Session.SetString(Username, username);*/
                    Globals.LoggedInUser = item;
                    return RedirectToPage("/ProductPage");
                }
                //else
                //{
                //    LoginError = "Wrong username or password try again!";
                //   
                //}
            }
            return Page();

        }
    }
}
