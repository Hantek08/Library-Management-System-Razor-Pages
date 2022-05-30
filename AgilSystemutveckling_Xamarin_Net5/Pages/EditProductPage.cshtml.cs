using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class EditProductPageModel : PageModel
    {
        public static Models.Products EditProduct = new Models.Products();
        public void OnGet()
        {
            EditProduct = DescriptionPageModel.product;
        }

        public IActionResult OnPostSaveChanges()
        {
            EditProduct.Description = Request.Form["newDescription"];
            Service.UpdateService.Update.Product(EditProduct);
            return RedirectToPage("./DescriptionPage", new { id = DescriptionPageModel.productId });
        }
    }
}
