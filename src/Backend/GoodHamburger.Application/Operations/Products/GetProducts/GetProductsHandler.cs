using Ardalis.Result;
using AutoMapper;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Products.GetProducts
{
    public class GetProductsHandler(IMapper _mapper, IProductRepository _productRepository) : IRequestHandler<GetProductsCommand, Result<List<ProductDto>>>
    {
        public async Task<Result<List<ProductDto>>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProducts(cancellationToken);

            if (products.IsSuccess)
            {
                var result = _mapper.Map<List<ProductDto>>(products.Value);
                return Result.Success(result);
            }

            return Result.Error(products.Errors.First());        
        }
    }
}
