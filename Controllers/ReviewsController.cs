using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTO;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public IActionResult AddReview([FromBody] ReviewDto model)
        {
            try
            {
                var review = _reviewService.AddReview(model, userId: 1); // Hardcoded userId for simplicity
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
