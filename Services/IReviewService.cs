using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;

namespace OrderManagementSystem.Services
{
    public interface IReviewService
    {
        Review AddReview(ReviewDto model, int userId);
    }
}