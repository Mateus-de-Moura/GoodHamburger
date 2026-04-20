using GoodHamburger.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
    }
}
