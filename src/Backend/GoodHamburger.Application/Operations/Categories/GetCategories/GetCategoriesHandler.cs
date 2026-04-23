using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Categories.GetCategories
{
    public class GetCategoriesHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<GetCategoriesCommand, Result<List<CategoryDto>>>
    {
        public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesCommand request, CancellationToken cancellationToken)
        {
            var categoriesResult = await categoryRepository.GetAllCategories(cancellationToken);

            if (!categoriesResult.IsSuccess)
                return Result<List<CategoryDto>>.Error(string.Join(" | ", categoriesResult.Errors));

            var mapped = categoriesResult.Value
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Description = c.Description
                })
                .ToList();

            return Result.Success(mapped);
        }
    }
}
