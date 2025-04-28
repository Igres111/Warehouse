using Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields
        public readonly IProduct _ProductMethods;
        #endregion

        #region Constructor
        public ProductController(IProduct ProductMethods)
        {
            _ProductMethods = ProductMethods;
        }
        #endregion

        #region POST Endpoints
        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct(AddProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _ProductMethods.AddProduct(product);
            return Ok("Product added successfully");
        }
        #endregion

        #region GET Endpoints
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

        [HttpGet("Find-Product-By-Name")]
        public async Task<IActionResult> FindProductsByName(string name)
        {
            var result = await _ProductMethods.FindProductsByName(name);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            return Ok(result);
        }
    }
    #endregion
}
