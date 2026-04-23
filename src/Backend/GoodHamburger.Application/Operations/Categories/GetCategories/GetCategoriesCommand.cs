using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using MediatR;

namespace GoodHamburger.Application.Operations.Categories.GetCategories
{
    public class GetCategoriesCommand : IRequest<Result<List<CategoryDto>>>
    {
    }
}
