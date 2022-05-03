using AgilSystemutveckling_Xamarin_Net5.Models;
namespace AgilSystemutveckling_Xamarin_Net5.Services
{
    public interface IProductRepository
    {
        IEnumerable<Products> GetAllProducts();
        Products GetProductById(int id);
        IEnumerable<Products> SearchProduct(string searchString);
        Products DeleteProduct(int id);
    }
}
