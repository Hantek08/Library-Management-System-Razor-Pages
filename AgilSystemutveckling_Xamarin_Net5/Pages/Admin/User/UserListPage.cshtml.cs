using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.Admin.User
{
    public class UserListPageModel : PageModel
    {
        //List that is shown to admin and employee
        public static List<Models.Users> UserList;
        
        [BindProperty]
        public Models.Users newUser { get; set; }

        public void OnGet()
        {
            UserList = Service.GetService.Get.GetAllUsers();
        }

        public void OnPost()
        {
            ViewData["User"] = UserList;
            // git test
        }

        public IActionResult OnPostAdd()
        {   
            if (!ModelState.IsValid)
            {
               return Page();
            }

            Service.CreateService.Create.AddUser(newUser);
            return RedirectToPage("/Admin/User/UserListPage");
        }


    }
}
