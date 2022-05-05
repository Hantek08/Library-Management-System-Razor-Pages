using AgilSystemutveckling_Xamarin_Net5.TestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    [BindProperties]
    public class TestPageModel : PageModel
    {

        public List<Author> Authors { get; set; }
        public void OnGet()
        {
            Authors = TestService.GetService.Get.GetAllAuthors();
        }
    }
}
