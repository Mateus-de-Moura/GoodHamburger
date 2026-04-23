using Ardalis.Result;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data.Repositories
{
    public class CategoryRepository(GoodHamburgerDbContext context) : ICategoryRepository
    {
        public async Task<Result<List<Category>>> GetAllCategories(CancellationToken ct)
        {
            var categories = await context.Categories
                .AsNoTracking()
                .Where(c => c.Active)
                .OrderBy(c => c.Description)
                .ToListAsync(ct);

            return categories.Count > 0
                ? Result.Success(categories)
                : Result<List<Category>>.NotFound("Nenhuma categoria cadastrada.");
        }
    }
}
