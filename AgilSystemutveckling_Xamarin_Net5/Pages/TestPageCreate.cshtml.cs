using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AgilSystemutveckling_Xamarin_Net5.TestModels;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    [BindProperties]
    public class TestPageCreateModel : PageModel
    {
        public Author Author { get; set; }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            Author = testService.CreateService.Create.AddAuthor(Author);

            return RedirectToPage("TestPage");
        }
    }
}
