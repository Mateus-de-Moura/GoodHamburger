#nullable enable

using Ardalis.Result;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Result;
using System.Linq.Expressions;

namespace GoodHamburger.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Result<List<Product>>> GetAllProducts(CancellationToken ct);
        Task<Result<List<Product>>> GetProductsByIds(IEnumerable<Guid> ids, CancellationToken ct);
        Task<Result<PagedList<Product>>> GetPaged(int pageNumber, int pageSize, CancellationToken ct, Expression<Func<Product, bool>>? expression = null);
        Task<Result<Product>> CreateProduct(Product product, CancellationToken ct);
        Task<Result<Product>> GetProductById(Guid id, CancellationToken ct);
        Task<Result<bool>> UpdateProduct(Product product, CancellationToken ct);
        Task<Result<bool>> DeleteProduct(Guid id, CancellationToken ct);
    }
}
