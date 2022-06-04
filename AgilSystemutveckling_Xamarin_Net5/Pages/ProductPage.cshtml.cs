using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Models;
using AgilSystemutveckling_Xamarin_Net5.Service.GetService;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class SearchPageModel : PageModel
    {
        public static List<Products?> BookName;
        public List<SelectListItem> Options { get; set; }

        [BindProperty]
        public Products newBook { get; set; }
        
        [BindProperty]
        public Products EditProducts { get; set; }

        // Get a list of all products
        public void OnGet()
        {
            BookName = (List<Products?>)Get.GetAllProducts().Where(c => c.CategoryName != "Event").ToList();
            Options = Service.GetService.Get.GetAllCategories().Select(c => 
                                                                        new SelectListItem
                                                                        {
                                                                            Text = c.CategoryName,
                                                                            Value = c.CategoryName
                                                                        }).ToList();
              
        }

        // OnGet for later edits
        public void OnGetProdById(int id)
        {
            EditProducts = Get.GetProductById(id);

        }
        public void OnPost()
        {
            ViewData["Books"] = BookName;
            // git test
        }

        // Be able to edit a specific product
        public IActionResult OnPostEdit()
        {
            Service.UpdateService.Update.UpdateUnitsInStock(EditProducts.Id, EditProducts.UnitsInStock);
            if (ModelState.IsValid)
            {
                return RedirectToPage("/ProductPage");
            }
            return Page();
        }
        
        // Add a new product
        public IActionResult OnPostAdd()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Service.CreateService.Create.AddProduct(newBook);
            return RedirectToPage("/ProductPage");
        }

        // Adds product to cart
        public void OnPostAddToCart(int id)
        {
            BookName = Get.GetAllProducts();
            var product = BookName.Where(c => c.Id == id).ToList();
            Globals.CartList.Add(product[0]);
            TempData["success"] = product[0].Title + " has been added"; // Popup

        }

        // Be able to make product hidden from customer
        public IActionResult OnPostMakeProductHidden(int id)
        {
            var product = Service.GetService.Get.GetProductById(id);
            product.Active = 0;
            product.Id = id;
            Service.UpdateService.Update.Product(product);
            return RedirectToPage("/ProductPage");
        }

        // Be able to unhide product from customer
        public IActionResult OnPostMakeProductUnhidden(int id)
        {
            var product = Service.GetService.Get.GetProductById(id);
            product.Active = 1;
            product.Id = id;
            Service.UpdateService.Update.Product(product);
            return RedirectToPage("/ProductPage");
        }

        // Be able to make a booking for a product
        public void OnPostBookProduct(int id)
        {
             var product = BookName.Where(c => c.Id == id).ToList();
            Service.CreateService.Create.AddHistory(Globals.LoggedInUser.Id, id, 7);
              
             TempData["success"] = product[0].Title + " has been booked";
        }
    }
}
