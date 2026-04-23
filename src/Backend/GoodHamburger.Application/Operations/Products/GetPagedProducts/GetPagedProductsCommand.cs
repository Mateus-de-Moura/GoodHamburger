using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Result;
using MediatR;

namespace GoodHamburger.Application.Operations.Products.GetPagedProducts
{
    public class GetPagedProductsCommand : IRequest<Result<PagedList<ProductDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
