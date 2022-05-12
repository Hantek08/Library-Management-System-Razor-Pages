using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class FransTestModel : PageModel
    {
        public void OnGet()
        {
            Models.Products product = new Models.Products() { Title = "Lord of the rings", Description = "The Lord of the Rings is the saga of a group of sometimes reluctant heroes who set forth to save their world from consummate evil. Its many worlds and creatures were drawn from Tolkiens extensive knowledge of philology and folklore.",
            AuthorName = "J. R. R. Tolkien", CategoryName = Models.CategoryNames.Book.ToString(), SubCategoryName = Models.SubCategoryNames.Fiction.ToString(), UnitsInStock = 40,
            ImgUrl = "https://s26162.pcdn.co/wp-content/uploads/2017/05/the-lord-of-the-rings-book-cover.jpg"
            };

            Service.CreateService.Create.AddProduct(product);
        }
    }
}
