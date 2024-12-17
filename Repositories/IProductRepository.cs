using OrderManagementSystem.Model;

namespace OrderManagementSystem.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        List<Product> GetAll();
        Product GetById(int id);
        List<Product> GetFilteredProducts(string name, decimal minPrice, decimal maxPrice, int page, int pageSize);
        Product GetProductById(int id);
        void UpdateProduct(Product product);
    }
}