using AgilSystemutveckling_Xamarin_Net5.Data;
using AgilSystemutveckling_Xamarin_Net5.Models;

namespace AgilSystemutveckling_Xamarin_Net5.Services
{
    public class ProductRepository : IProductRepository
    {
        private List<Products> _productList;
        private readonly LibraryDbContext _dbContext;
        public ProductRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            _productList = _dbContext.Products.ToList();

        }
        public IEnumerable<Products> GetAllProducts()
        {
            return _productList;
        }

        public Products GetProductById(int id)
        {
            return _productList.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Products> SearchProduct(string searchString)
        {
            return _productList.Where(p => p.Title.Contains(searchString)).ToList();
        }

        public async void DeleteProduct(int id)
        {
            var productToDelete = GetProductById(id);
            if(productToDelete != null)
            {
                _dbContext.Remove(productToDelete);
              await  _dbContext.SaveChangesAsync();
            }
            

        }

        public async void AddProduct(Products product)
        {
            product.Id = _productList.Max(p => p.Id) +1;
            _dbContext.Add(product);
            await _dbContext.SaveChangesAsync();
        }

     
    }
}
