using Ardalis.Result;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Result;
using GoodHamburger.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Infrastructure.Data.Repositories
{
    public class ProductRepository(GoodHamburgerDbContext context) : IProductRepository
    {
        public async Task<Result<Product>> CreateProduct(Product product, CancellationToken ct)
        {
            var existProdutWithSameDescription = await context.Products
                .AnyAsync(p => p.Description == product.Description, ct);

            if (existProdutWithSameDescription)
                return Result<Product>.Conflict(["Já existe um produto cadastrado com a mesma descrição"]);

            var category = await context.Categories.FindAsync([product.CategoryId], ct);

            if (category is null)
                return Result<Product>.NotFound("Categoria não encontrada");

            await context.Products.AddAsync(product, ct);

            var rowsAffected = await context.SaveChangesAsync(ct);

            return rowsAffected > 0 ?
                  Result<Product>.Success(product) :
                  Result<Product>.Error("Ocorreu um erro ao cadastrar o produto");

        }

        public async Task<Result<bool>> DeleteProduct(Guid id, CancellationToken ct)
        {
            var existingProduct = await context.Products.FindAsync([id], ct);

            if (existingProduct is null)
                return Result<bool>.NotFound("Produto não encontrado na base.");

            var rowsAffected = await context.Products
                .Where(x => x.Id.Equals(id))
                .ExecuteUpdateAsync(prop => prop.SetProperty(x => x.Active, false), ct);

            return rowsAffected > 0 ?
                  Result.Success(true) :
                  Result.Error("Ocorreu um erro ao deletar o produto");
        }

        public async Task<Result<List<Product>>> GetAllProducts(CancellationToken ct)
        {
            var products = await context.Products
                .AsNoTracking()
                .Where(x => x.Active)
                .Include(x => x.Category)
                .Include(x => x.ProductImage)
                .ToListAsync(ct);

            return products.Count > 0 ?
                  Result<List<Product>>.Success(products) :
                  Result<List<Product>>.NotFound("Nenhum produto cadastrado na base");
        }

        public async Task<Result<List<Product>>> GetProductsByIds(IEnumerable<Guid> ids, CancellationToken ct)
        {
            var normalizedIds = (ids ?? Enumerable.Empty<Guid>())
                .Where(id => id != Guid.Empty)
                .Distinct()
                .ToList();

            if (normalizedIds.Count == 0)
                return Result<List<Product>>.Error("Informe ao menos um id de produto válido.");

            var products = await context.Products
                .AsNoTracking()
                .Where(x => x.Active && normalizedIds.Contains(x.Id))
                .Include(x => x.Category)
                .ToListAsync(ct);

            return products.Count > 0
                ? Result.Success(products)
                : Result<List<Product>>.NotFound("Nenhum produto encontrado para os ids informados.");
        }

        public async Task<Result<PagedList<Product>>> GetPaged(int pageNumber, int pageSize, CancellationToken ct, Expression<Func<Product, bool>>? expression = null)
        {
            var queryable = context.Products
                .AsNoTracking()
                .Where(x => x.Active)
                .Include(x => x.Category)
                .Include(x => x.ProductImage)
                .AsQueryable();

            if (expression is not null)
                queryable = queryable.Where(expression).AsQueryable();

            var count = await queryable.CountAsync(ct);

            var rows = await queryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var paged = new PagedList<Product>(pageNumber, pageSize, count)
            {
                CurrentItems = rows ?? Enumerable.Empty<Product>(),
            };

            return Result.Success(paged);
        }

        public async Task<Result<Product>> GetProductById(Guid id, CancellationToken ct)
        {
            var product = await context.Products
                .AsNoTracking()
                .Where(x => x.Active)
                .Include(x => x.Category)
                .Include(x => x.ProductImage)
                .FirstOrDefaultAsync(x => x.Id.Equals(id), ct);

            return product is not null ?
                  Result<Product>.Success(product) :
                  Result<Product>.NotFound("Produto não encontrado na base");
        }

        public async Task<Result<bool>> UpdateProduct(Product product, CancellationToken ct)
        {
            var existingProduct = await context.Products
                .Where(x => x.Active)
                .Include(x => x.Category)
                .Include(x => x.ProductImage)
                .FirstOrDefaultAsync(x => x.Id.Equals(product.Id), ct);

            if (existingProduct is null)
                return Result.Error("Produto não cadastrado na base.");

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;

            if (product.ProductImage != null)
            {
                if (existingProduct.ProductImage == null)
                {
                    existingProduct.ProductImage = new ProductImage
                    {
                        FileName = product.ProductImage.FileName,
                        ContentType = product.ProductImage.ContentType,
                        ImageBytes = product.ProductImage.ImageBytes
                    };
                }
                else
                {
                    existingProduct.ProductImage.FileName = product.ProductImage.FileName;
                    existingProduct.ProductImage.ContentType = product.ProductImage.ContentType;
                    existingProduct.ProductImage.ImageBytes = product.ProductImage.ImageBytes;
                }
            }

            try
            {
                var rowsAffected = await context.SaveChangesAsync(ct);

                return Result.Success(true);
                     
            }
            catch (Exception)
            {
                return Result.Error("Ocorreu um erro ao atualizar o produto");
            }

            
        }
    }
}
