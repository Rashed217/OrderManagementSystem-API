using OrderManagementSystem.Model;

namespace OrderManagementSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public Product GetById(int id)
        {
            return _context.Products.SingleOrDefault(p => p.PID == id);
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}
