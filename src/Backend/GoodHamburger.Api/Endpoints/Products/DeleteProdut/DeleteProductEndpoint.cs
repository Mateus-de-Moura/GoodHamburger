using FastEndpoints;
using GoodHamburger.Application.Operations.Products.DeleteProducts;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Products.DeleteProdut
{
    public class DeleteProductEndpoint(IMediator mediator) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Delete("/products/{id:guid}");
            AllowAnonymous();
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var result = await mediator.Send(new DeleteProductCommand(id), ct);

            if (result.IsSuccess)
                await Send.OkAsync(new { message = "Produto deletado com sucesso." }, ct);
            else
                await Send.OkAsync(new { message = result.Errors.FirstOrDefault() }, ct);
        }
    }
}

