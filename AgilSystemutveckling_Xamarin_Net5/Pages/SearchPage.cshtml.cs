using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Models;
using AgilSystemutveckling_Xamarin_Net5.Service.GetService;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class SearchPageModel : PageModel
    {
        public static List<Products?> BookName;
        [BindProperty]
        public Products newBook { get; set; }
        public void OnGet()
        {
            BookName = Get.GetAllProducts();
            foreach (var book in BookName)
            {
                if(book.CategoryName == "Event")
                    BookName.Remove(book);
            }
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

        public void OnPostAddToCart(int id)
        {
            BookName = Get.GetAllProducts();
            var product = BookName.Where(c => c.Id == id).ToList();
            Globals.CartList.Add(product[0]);
            
        }
    }
}
