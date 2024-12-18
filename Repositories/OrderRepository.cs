using OrderManagementSystem.Model;

namespace OrderManagementSystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public Order GetById(int id)
        {
            return _context.Orders.SingleOrDefault(o => o.OID == id);
        }

        public List<Order> GetByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddOrderProduct(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();
        }
    }
}
