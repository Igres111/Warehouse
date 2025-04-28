using Azure;
using DataAccess.Context;
using DataAccess.Entities;
using Dtos.ProductDtos;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Warehouse.Helpers;

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
        public async Task<ProductResponse> AddProduct(AddProductDto product)
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
            return new ProductResponse { Id= newProduct.Id, Message = "Product added successfully" };
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

        public async Task<ReceiveProductDto> FindProductsByName(string name)
        {
            var product = await _context.products.FirstOrDefaultAsync(x => x.Name == name);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var productForUser = new ReceiveProductDto()
            {
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                QuantityInStock = product.QuantityInStock,
                Price = product.Price,
            };
            return productForUser;
        }

        public async Task<List<ReceiveProductDto>> FilterProductsByCategory(string category)
        {
            var products = await _context.products.Where(x => x.Category == category).ToListAsync();
            if (products == null)
            {
                throw new Exception("No products found in this category");
            }
            var filteredProducts = products.Select(x => new ReceiveProductDto()
            {
                Name = x.Name,
                Description = x.Description,
                Category = x.Category,
                QuantityInStock = x.QuantityInStock,
                Price = x.Price,
            }).ToList();
            return filteredProducts;
        }
        #endregion
    }
}
