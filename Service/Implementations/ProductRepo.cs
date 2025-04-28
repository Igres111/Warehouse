using DataAccess.Context;
using DataAccess.Entities;
using Dtos.ProductDtos;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ProductRepo : IProduct
    {
        #region Fields
        public readonly AppDbContext _context;
        #endregion

        #region Constructor
        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task AddProduct(AddProductDto product)
        {
            var exists = await _context.products.FirstOrDefaultAsync(x => x.Name == product.Name);
            if(exists != null)
            {
                throw new Exception("Product already exists");
            }
            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                QuantityInStock = product.QuantityInStock,
                Price = product.Price,
                CreatedAt = DateTime.UtcNow,
            };
            await _context.products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Product>> GetAllProducts()
        {
            var allProducts = await _context.products.ToListAsync();
            if(allProducts == null)
            {
                throw new Exception("No products found");
            }
            return allProducts;
        }

        #endregion
    }
}
