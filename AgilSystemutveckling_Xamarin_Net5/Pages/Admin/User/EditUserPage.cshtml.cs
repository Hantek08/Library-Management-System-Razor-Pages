using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.Admin.User
{
    public class EditUserPageModel : PageModel
    {
        public static Models.Users UserInfo = new Models.Users();
        public void OnGet(int id)
        {
             UserInfo = Service.GetService.Get.GetAllUsers().Where(x => x.Id == id).FirstOrDefault();
        }

        public void OnPostBlock()
        {
            UserInfo.Blocked = true;
            Service.UpdateService.Update.User(UserInfo);
        }
        public void OnPostUnblock()
        {
            UserInfo.Blocked = false;
            Service.UpdateService.Update.User(UserInfo);
        }
    }
}
