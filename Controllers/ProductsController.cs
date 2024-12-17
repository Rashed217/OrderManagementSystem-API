using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTO;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto model)
        {
            try
            {
                var product = _productService.AddProduct(model);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public IActionResult GetProducts([FromQuery] ProductFilterDto filter)
        {
            try
            {
                var products = _productService.GetFilteredProducts(
                    filter.Name, filter.MinPrice, filter.MaxPrice, filter.Page, filter.PageSize);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
