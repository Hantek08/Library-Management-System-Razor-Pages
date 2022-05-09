
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AgilSystemutveckling_Xamarin_Net5.TestModels;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.TestPages
{
    [BindProperties]
    public class TestPageCreateModel : PageModel
    {
        public Models.Author Author { get; set; }
        public IActionResult OnPost()
        {
            Author = testService.CreateService.Create.AddAuthor(Author);
            return RedirectToPage("TestPage");
        }
    }
}
