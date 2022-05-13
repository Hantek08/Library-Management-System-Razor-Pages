using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.Admin.User
{
    public class UserListPageModel : PageModel
    {
        //List that is shown to admin and employee
        public static List<TestModels.User> UserList;
        
        [BindProperty]
        public TestModels.User newUser { get; set; }

        public void OnGet()
        {
            UserList = TestService.GetService.Get.GetAllUsers();
        }

        public void OnPost()
        {
            ViewData["User"] = UserList;
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
