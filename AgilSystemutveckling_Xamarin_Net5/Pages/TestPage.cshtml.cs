using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AgilSystemutveckling_Xamarin_Net5.TestModels;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class TestPageModel : PageModel
    {
        public static List<Author> Authors { get; set; } 
        public void OnGet()
        {
            Authors = TestService.GetService.Get.GetAllAuthors();
        }
    }
}
