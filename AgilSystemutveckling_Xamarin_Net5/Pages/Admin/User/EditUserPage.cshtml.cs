using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.Admin.User
{
    public class EditUserPageModel : PageModel
    {
        public static Models.Users UserInfo = new Models.Users();

        // Get chosen user by id
        public void OnGet(int id)
        {
             UserInfo = Service.GetService.Get.GetAllUsers().Where(x => x.Id == id).FirstOrDefault();
        }

        //  Block user
        public void OnPostBlock()
        {
            UserInfo.Blocked = true;
            Service.UpdateService.Update.User(UserInfo);
            if (UserInfo.Blocked == true)
            TempData["success"] = UserInfo.Username + " has been blocked ";
        }

        // Unblock user
        public void OnPostUnblock()
        {
            UserInfo.Blocked = false;
            Service.UpdateService.Update.User(UserInfo);
            TempData["success"] = UserInfo.Username + " has been unblocked ";
        }

        // Update address
        public void OnPostSaveAddress()
        {
            UserInfo.Address = Request.Form["newAddress"];
            Service.UpdateService.Update.User(UserInfo);
            TempData["success"] = "New Address saved"; // Popup
        }

        // Update Password
        public void OnPostSavePassword()
        {
            UserInfo.Password = Request.Form["newPassword"];
            Service.UpdateService.Update.User(UserInfo);
            TempData["success"] = "New Password saved";
            /*return RedirectToPage(".Admin/User/UserListPage", new { id = DescriptionPageModel.productId });*/
        }

    }
}
