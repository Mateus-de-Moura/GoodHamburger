using Ardalis.Result;
using AutoMapper;
using GoodHamburger.Application.Shared;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Operations.Products.UpdateProducts
{
    public class UpdateProductHandler(IProductRepository productRepository, IMapper mapper ) : IRequestHandler<UpdateProductCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new UpdateProductValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<bool>.Error(string.Join(" | ", errors));
            }

            var product = mapper.Map<Product>(request);

            if (request.Image != null)
            {
                product.ProductImage!.ImageBytes =
                    await Converts.FormFileToByteArrayAsync(request.Image);
            }

            var result = await productRepository.UpdateProduct(product, cancellationToken);

            return result.IsSuccess ?
               Result.Success(true) : 
               Result<bool>.Error(string.Join(" | ", result.Errors));
        }
    }
}

