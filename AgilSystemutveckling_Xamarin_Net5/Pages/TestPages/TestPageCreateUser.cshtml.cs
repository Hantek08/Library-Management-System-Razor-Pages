
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AgilSystemutveckling_Xamarin_Net5.TestModels;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.TestPages
{
    [BindProperties]
    public class TestPageCreateUserModel : PageModel
    {
        public User user { get; set; }
        public IActionResult OnPost()
        {
            user = testService.CreateService.Create.AddUser(user);
            return RedirectToPage("TestPage");
        }
    }
}
