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
        
        [BindProperty]
        public Products EditProducts { get; set; }
        public void OnGet()
        {
            BookName = (List<Products?>)Get.GetAllProducts().Where(c => c.CategoryName != "Event").ToList();
        }

        public void OnGetProdById(int id)
        {
            EditProducts = Get.GetProductById(id);

        }
        public void OnPost()
        {
            ViewData["Books"] = BookName;
            // git test
        }
        
        public IActionResult OnPostEdit()
        {
            Service.UpdateService.Update.UpdateUnitsInStock(EditProducts.Id, EditProducts.UnitsInStock);
            if(ModelState.IsValid)
            {
                return RedirectToPage("/ProductPage");
            }
            return Page();
        }
        public IActionResult OnPostAdd()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Service.CreateService.Create.AddProduct(newBook);
            return RedirectToPage("/ProductPage");
        }

        public void OnPostAddToCart(int id)
        {
            BookName = Get.GetAllProducts();
            var product = BookName.Where(c => c.Id == id).ToList();
            Globals.CartList.Add(product[0]);
            TempData["success"] = product[0].Title + " has been added";

        }

        public IActionResult OnPostMakeProductHidden(int id)
        {
            var product = Service.GetService.Get.GetProductById(id);
            product.Active = 0;
            product.Id = id;
            Service.UpdateService.Update.Product(product);
            return RedirectToPage("/ProductPage");
        }
        public IActionResult OnPostMakeProductUnhidden(int id)
        {
            var product = Service.GetService.Get.GetProductById(id);
            product.Active = 1;
            product.Id = id;
            Service.UpdateService.Update.Product(product);
            return RedirectToPage("/ProductPage");
        }
    }
}
