using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.Admin.User
{
    public class UserListPageModel : PageModel
    {
        public static List<TestModels.User> UserList;

        public void OnGet()
        {
            UserList = TestService.GetService.Get.GetAllUsers();
        }

        public void OnPost()
        {
            ViewData["User"] = UserList;
            // git test
        }
    }
}
