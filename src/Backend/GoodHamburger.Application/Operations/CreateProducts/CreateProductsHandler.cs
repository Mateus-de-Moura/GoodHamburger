using Ardalis.Result;
using GoodHamburger.Application.Shared;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.CreateProducts
{
    public class CreateProductsHandler(IProductRepository productRepository) : IRequestHandler<CreateProductsCommand, Result<string>>
    {       
        public async Task<Result<string>> Handle(CreateProductsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new CreateProducValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<string>.Error(errors.ToString());
            }

            var result = await productRepository.CreateProduct(new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ProductImage = new ProductImage
                {
                    ImageBytes = request.Image != null ? await Converts.FormFileToByteArrayAsync(request.Image) : null,
                    FileName = request.Image != null ? request.Image.FileName : null,
                    ContentType = request.Image != null ? request.Image.ContentType : null
                },
                CategoryId = request.CategoryId
            });

            return result.IsSuccess ? 
                Result<string>.Success("Produto cadastrado com sucesso") 
                : Result<string>.Error(result.Errors.ToString());
        }
    }
}
