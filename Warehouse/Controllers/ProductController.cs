using Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProduct _ProductMethods;
        public ProductController(IProduct ProductMethods)
        {
            _ProductMethods = ProductMethods;
        }
        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct(AddProductDto product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _ProductMethods.AddProduct(product);
            return Ok("Product added successfully");
        }
        [HttpGet("Get-All-Products")]
        public async Task<IActionResult> GetAllProducts()
        {
           var result = await _ProductMethods.GetAllProducts();
           if (result == null)
           {
                return NotFound("No products found");
           }
           return Ok(result);
        }
    }
}
