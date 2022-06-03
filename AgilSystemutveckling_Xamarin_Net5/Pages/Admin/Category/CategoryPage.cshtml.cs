using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Models;

namespace AgilSystemutveckling_Xamarin_Net5.Pages.Admin.Category
{
    public class CategoryPageModel : PageModel
    {
        public static List<Categories> CategoryList;
        [BindProperty]
        public Categories NewCategories { get; set; }

        //Get all categories
        public void OnGet()
        {
            CategoryList = Service.GetService.Get.GetAllCategories();
        }

        public void OnPost()
        {
            ViewData["Category"] = CategoryList;
        }

        // Add a category
        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Service.CreateService.Create.AddCategory(NewCategories);
            return RedirectToPage("/Admin/Category/CategoryPage");
        }


    }
}
