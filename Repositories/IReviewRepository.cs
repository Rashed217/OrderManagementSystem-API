using OrderManagementSystem.Model;

namespace OrderManagementSystem.Repositories
{
    public interface IReviewRepository
    {
        void AddReview(Review review);
        Review GetReviewByUserAndProduct(int userId, int productId);
        IEnumerable<Review> GetReviewsByProductId(int productId);
    }
}