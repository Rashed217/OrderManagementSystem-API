using OrderManagementSystem.Model;

namespace OrderManagementSystem.Repositories
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        void AddOrderProduct(OrderProduct orderProduct);
        Order GetById(int id);
        List<Order> GetByUserId(int userId);
    }
}