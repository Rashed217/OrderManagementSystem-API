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

        public Product GetProductById(int id)
        {
            // Using EF Core to query the product by its ID
            return _context.Products.SingleOrDefault(p => p.PID == id);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }


        public List<Product> GetFilteredProducts(string name, decimal minPrice, decimal maxPrice, int page, int pageSize)
        {
            return _context.Products
                .Where(p => (string.IsNullOrEmpty(name) || p.Name.Contains(name)) &&
                            (minPrice == 0 || p.Price >= minPrice) &&
                            (maxPrice == 0 || p.Price <= maxPrice))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
