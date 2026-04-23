using FastEndpoints;
using GoodHamburger.Application.Operations.Products.UpdateProducts;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Products.UpdateProduct
{
    public class UpdateProductEndpoint(IMediator mediator, AutoMapper.IMapper mapper) : Endpoint<UpdateProductRequest>
    {
        public override void Configure()
        {
            Put("/products");
            AllowAnonymous();
            AllowFileUploads();
        }
        public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
        {
            var mapped = mapper.Map<UpdateProductCommand>(req);

            var result = await mediator.Send(mapped, ct);

            if(result.IsSuccess)
                await Send.OkAsync(new {message = "Produto atualizado com sucesso."},ct);
            else
                await Send.OkAsync(result.Errors, ct);
        }
    }
}
