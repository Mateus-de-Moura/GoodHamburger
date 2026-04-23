using Ardalis.Result;
using AutoMapper;
using GoodHamburger.Application.Shared;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Products.CreateProducts
{
    public class CreateProductsHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<CreateProductsCommand, Result<string>>
    {       
        public async Task<Result<string>> Handle(CreateProductsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new CreateProducValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<string>.Error(string.Join(" | ", errors));
            }

            var product = mapper.Map<Product>(request);

            if (product.ProductImage is null)
            {
                product.ProductImage = new ProductImage
                {
                    FileName = "placeholder.bin",
                    ContentType = "application/octet-stream"
                };
            }

            product.ProductImage.ProductId = product.Id;
            product.ProductImageId = product.ProductImage.Id;

            if (request.Image != null)
                product.ProductImage.ImageBytes = await Converts.FormFileToByteArrayAsync(request.Image);
            else
                product.ProductImage.ImageBytes = Array.Empty<byte>();

            var result = await productRepository.CreateProduct(product, cancellationToken);

            return result.IsSuccess ? 
                Result<string>.Success("Produto cadastrado com sucesso") 
                : Result<string>.Error(string.Join(" | ", result.Errors));
        }
    }
}
