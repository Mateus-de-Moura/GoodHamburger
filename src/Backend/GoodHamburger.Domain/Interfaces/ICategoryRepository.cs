using Ardalis.Result;
using GoodHamburger.Domain.Entity;

namespace GoodHamburger.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Result<List<Category>>> GetAllCategories(CancellationToken ct);
    }
}
