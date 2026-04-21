using Ardalis.Result;
using GoodHamburger.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Interfaces
{
    public interface IProductRepository
    {
        public Task<Result<List<Product>>> GetAllProducts();
        public Task<Result<Product>> CreateProduct(Product product);
    }
}
