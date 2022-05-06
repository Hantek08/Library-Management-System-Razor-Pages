
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AgilSystemutveckling_Xamarin_Net5.TestModels;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.TestPages
{
    [BindProperties]
    public class TestPageCreateUserModel : PageModel
    {
        public User User{ get; set; }
        public IActionResult OnPost()
        {
            User = testService.CreateService.Create.AddUser(User);
            return RedirectToPage("TestPage");
        }
    }
}
