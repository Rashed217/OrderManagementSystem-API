using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;

namespace OrderManagementSystem.Services
{
    public interface IProductService
    {
        Product AddProduct(ProductDto model);
    }
}