using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Models;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class SearchPageModel : PageModel
    {
        public static List<Products> BookName;
        [BindProperty]
        public Products newBook { get; set; }
        public void OnGet()
        {
            BookName = Service.GetService.Get.GetAllProducts();
        }

        public void OnPost()
        {
            ViewData["Books"] = BookName;
            // git test
        }

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Service.CreateService.Create.AddProduct(newBook);
            return RedirectToPage("/SearchPage");
        }

        public IActionResult OnPostAddToCart(Products product)
        {
            
            return RedirectToPage("/SearchPage");
        }
            
    }


}
