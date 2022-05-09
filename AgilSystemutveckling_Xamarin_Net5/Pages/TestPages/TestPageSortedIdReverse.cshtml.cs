using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AgilSystemutveckling_Xamarin_Net5.TestModels;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.TestPages
{
    [BindProperties]
    public class TestPageSortedIdReverseModel : PageModel
    {
        public List<Author> Authors { get; set; }
        public void OnGet()
        {
            Authors = TestService.GetService.Get.GetAllAuthorsReverseOrderId();
        }
    }
}
