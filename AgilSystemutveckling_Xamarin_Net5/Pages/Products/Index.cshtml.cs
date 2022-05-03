using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Models;
using AgilSystemutveckling_Xamarin_Net5.Data;
using AgilSystemutveckling_Xamarin_Net5.Services;
namespace AgilSystemutveckling_Xamarin_Net5.Pages.Products
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Models.Products> Products { get; set; }
        private readonly IProductRepository _productRepository;

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OnGet()
        {
            Products = _productRepository.GetAllProducts();
        }
        public string SearchString { get; set; }
        public void OnPost(string searchString)
        {
            SearchString = searchString;
            Products = _productRepository.SearchProduct(SearchString);
        }

        public IActionResult OnGetDelet(string id)
        {
            _productRepository.DeletProduct(id);
            return RedirectToPage("Index");
        }

        /*public List<Models.Products> ProductsList { get; set; }
        private readonly LibraryDbContext _dbContext;
        public IndexModel(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //this handels HTTP Get request from the browser 
        public void OnGet()
        {
            ProductsList = _dbContext.Products.ToList();
        }

        public IActionResult OnGetDelet(string id)
        {
            var product = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToPage("Index");
        }*/
    }
}
