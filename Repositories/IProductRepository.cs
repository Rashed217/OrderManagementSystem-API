using OrderManagementSystem.Model;

namespace OrderManagementSystem.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        List<Product> GetAll();
        Product GetById(int id);
    }
}