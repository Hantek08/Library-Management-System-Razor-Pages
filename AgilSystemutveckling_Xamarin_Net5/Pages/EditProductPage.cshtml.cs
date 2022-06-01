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
            EditProduct.Id = DescriptionPageModel.productId;
        }

        public IActionResult OnPostSaveChanges()
        {
            int stockNumberHolder;

            EditProduct.Description = Request.Form["newDescription"];
            int.TryParse(Request.Form["newStockAmount"], out stockNumberHolder);
            EditProduct.UnitsInStock = stockNumberHolder;
            Service.UpdateService.Update.Product(EditProduct);
            TempData["success"] = "Changes has been saved";
            return RedirectToPage("./DescriptionPage", new { id = DescriptionPageModel.productId });
        }
    }
}
