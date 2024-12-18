using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;

namespace OrderManagementSystem.Services
{
    public interface IOrderService
    {
        Order PlaceOrder(OrderDto model, int userId);
    }
}