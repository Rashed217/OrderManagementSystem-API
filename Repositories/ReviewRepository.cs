using OrderManagementSystem.Model;

namespace OrderManagementSystem.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public Review GetReviewByUserAndProduct(int userId, int productId)
        {
            return _context.Reviews.SingleOrDefault(r => r.UserId == userId && r.ProductId == productId);
        }

        public IEnumerable<Review> GetReviewsByProductId(int productId)
        {
            return _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToList();
        }
    }
}
