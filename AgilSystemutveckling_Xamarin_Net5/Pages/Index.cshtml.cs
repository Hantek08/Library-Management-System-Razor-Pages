using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class IndexModel : PageModel
    {
        public static List<Models.Products> products { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

        }


       
        public void OnGet()
        {
            products = Service.GetService.Get.RecentlyAddedProducts(3); // To get top 3 products shown in start page
            Globals.LoggedInUser = new Models.Users();
            Globals.CartList = new List<Models.Products>();
        }

    }
}