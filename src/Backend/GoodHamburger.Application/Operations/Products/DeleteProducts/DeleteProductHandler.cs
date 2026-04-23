using Ardalis.Result;
using GoodHamburger.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Operations.Products.DeleteProducts
{
    public class DeleteProductHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result.Error("Produto não foi localizado.");

            var result = await productRepository.DeleteProduct(request.Id, cancellationToken);

            return result.IsSuccess ?
                Result.Success("Produto excluido com sucesso.") :
                Result.Error(result.Errors.ToString());
        }
    }
}
