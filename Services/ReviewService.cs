using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;
using OrderManagementSystem.Repositories;

namespace OrderManagementSystem.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IProductRepository _productRepo;

        public ReviewService(IReviewRepository reviewRepo, IProductRepository productRepo)
        {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
        }

        public Review AddReview(ReviewDto model, int userId)
        {
            // Check if product is purchased by user and review is not already given
            var existingReview = _reviewRepo.GetReviewByUserAndProduct(userId, model.ProductId);
            if (existingReview != null)
                throw new Exception("You have already reviewed this product.");

            var review = new Review
            {
                UserId = userId,
                ProductId = model.ProductId,
                Rating = model.Rating,
                Comment = model.Comment,
                ReviewDate = DateTime.UtcNow
            };

            _reviewRepo.AddReview(review);

            // Recalculate product's overall rating
            var product = _productRepo.GetById(model.ProductId);
            product.OverallRating = _reviewRepo.GetReviewsByProductId(model.ProductId)
                .Average(r => r.Rating);
            _productRepo.UpdateProduct(product);

            return review;
        }
    }
}
