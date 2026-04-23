using Ardalis.Result;
using AutoMapper;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Result;
using MediatR;

namespace GoodHamburger.Application.Operations.Products.GetPagedProducts
{
    public class GetPagedProductsHandler(IProductRepository productRepository, IMapper mapper)
        : IRequestHandler<GetPagedProductsCommand, Result<PagedList<ProductDto>>>
    {
        public async Task<Result<PagedList<ProductDto>>> Handle(GetPagedProductsCommand request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var pagedResult = await productRepository.GetPaged(pageNumber, pageSize, cancellationToken);

            if (!pagedResult.IsSuccess)
                return Result<PagedList<ProductDto>>.Error(string.Join(" | ", pagedResult.Errors));

            var currentItems = mapper.Map<List<ProductDto>>(pagedResult.Value.CurrentItems);

            var mapped = new PagedList<ProductDto>(pagedResult.Value.PageNumber, pagedResult.Value.PageSize, pagedResult.Value.TotalItems)
            {
                CurrentItems = currentItems
            };

            return Result<PagedList<ProductDto>>.Success(mapped);
        }
    }
}
