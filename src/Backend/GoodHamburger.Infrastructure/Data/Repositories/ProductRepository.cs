using Ardalis.Result;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Infrastructure.Data.Repositories
{
    public class ProductRepository(GoodHamburgerDbContext context) : IProductRepository
    {
        public async Task<Result<Product>> CreateProduct(Product product)
        {
            var existProdutWithSameDescription = await context.Products.AnyAsync(p => p.Description == product.Description);

            if (existProdutWithSameDescription)
                return Result<Product>.Conflict(["Já existe um produto cadastrado com a mesma descrição"]);

            var category = await context.Categories.FindAsync(product.CategoryId);

            if (category is null)
                return Result<Product>.NotFound("Categoria não encontrada");

            await context.Products.AddAsync(product);

            var rowsAffected = await context.SaveChangesAsync();

            return rowsAffected > 0 ?
                  Result<Product>.Success(product) :
                  Result<Product>.Error("Ocorreu um erro ao cadastrar o produto");

        }

        public async Task<Result<List<Product>>> GetAllProducts()
        {
            var products = await context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.ProductImage)
                .ToListAsync();

            return products.Count > 0 ?
                  Result<List<Product>>.Success(products) :
                  Result<List<Product>>.NotFound("Nenhum produto cadastrado na base");
        }
    }
}
