using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using MediatR;

namespace GoodHamburger.Application.Operations.GetHamburgers
{
    public class GetProductsCommand : IRequest<Result<List<ProductDto>>>
    {
    }
}
