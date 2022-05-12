using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Models;
using AgilSystemutveckling_Xamarin_Net5.Pages.Service.CreateService;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IndexModel()
        {
        }

        public void OnGet()
        {
           
        }
    }
}