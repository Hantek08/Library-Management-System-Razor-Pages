using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AgilSystemutveckling_Xamarin_Net5.TestModels;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    [BindProperties]
    public class TestPageSortedReverseModel : PageModel
    {
       
        public List<Author> Authors { get; set; }
        public void OnGet()
        {
            Authors = TestService.GetService.Get.GetAllAuthorsReverseOrder();
        }
    }
}
