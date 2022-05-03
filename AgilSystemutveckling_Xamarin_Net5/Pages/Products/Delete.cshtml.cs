using AgilSystemutveckling_Xamarin_Net5.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public DeleteModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [BindProperty]
        public Models.Products Products { get; set; }
        public IActionResult OnGet(int id)
        {
           Products = _productRepository.GetProductById(id);
            if(Products == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var deletedProduct = _productRepository.DeleteProduct(Products.Id);
            if(deletedProduct == null)
            {
                return RedirectToPage("/NotFound");

            }
            return RedirectToPage("Index");
        }
    }
}
