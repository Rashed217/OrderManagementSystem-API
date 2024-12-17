using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;

namespace OrderManagementSystem.Services
{
    public interface IProductService
    {
        Product AddProduct(ProductDto model);
        List<Product> GetFilteredProducts(string name, decimal minPrice, decimal maxPrice, int page, int pageSize);
        Product GetProductById(int id);
        Product UpdateProduct(int id, ProductDto model);
    }
}