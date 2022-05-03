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

        public static string username;
        public static string password;

        public void OnGet()
        {

        }


        public IActionResult OnPost()
        {
            Username = Request.Form["username"];
            Password = Request.Form["password"];
            username = Username;
            password = Password;

            if (Username.Equals("abc") && Password.Equals("123"))
            {
                /*HttpContext.Session.SetString("username", Username);
                HttpContext.Session.SetString(Username, username);*/
                return RedirectToPage("/UserPage");
            }
            else
            {
 
                return Page();
            }
        }
    }
}
