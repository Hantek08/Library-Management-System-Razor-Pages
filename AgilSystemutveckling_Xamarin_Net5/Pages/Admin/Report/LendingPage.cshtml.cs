using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.Admin.Report
{
    public class LendingPageModel : PageModel
    {
        public static List<Models.Users> UserLists;

        [BindProperty]
        public Models.Users newUser { get; set; }

        public void OnGet()
        {
            UserLists = Service.GetService.Get.GetAllUsers();
        }

        public void OnPost()
        {
            ViewData["User"] = UserLists;
            // git test
        }

        public ActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            /*  TestService.CreateService.Create.AddUser(newUser);*/
            return RedirectToPage("/Admin/User/UserListPage");
        }
    }
}
