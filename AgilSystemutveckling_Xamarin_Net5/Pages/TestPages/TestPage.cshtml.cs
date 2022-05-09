
using AgilSystemutveckling_Xamarin_Net5.TestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.TestPages
{
    [BindProperties]
    public class TestPageModel : PageModel
    {
        public List<User> Authors { get; set; }
        public void OnGet()
        {
            Authors = TestService.GetService.Get.GetAllUsers();
        }
    }
}
