using Ardalis.Result;
using AutoMapper;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Products.GetById
{
    public class GetProductByIdHandler(IMapper mapper, IProductRepository productRepository) : IRequestHandler<GetProductByIdCommand, Result<ProductDto>>
    {
        public async Task<Result<ProductDto>> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result.Error("Produto não encontrado, informar um Id válido.");

            var result = await productRepository.GetProductById(request.Id, cancellationToken);

            if (result.IsSuccess)
            {
                var dto = mapper.Map<ProductDto>(result.Value);
                return Result.Success(dto);
            }
            return Result.Error(result.Errors.First());
        }
    }
}
