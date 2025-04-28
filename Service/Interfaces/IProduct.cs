using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Dtos.ProductDtos;

namespace Service.Interfaces
{
    public interface IProduct
    {
        public Task AddProduct(AddProductDto product);
        public Task<List<Product>> GetAllProducts();
        public Task<ReceiveProductDto> FindProductsByName(string name);
        public Task<List<ReceiveProductDto>> FilterProductsByCategory(string category);
    }
}
