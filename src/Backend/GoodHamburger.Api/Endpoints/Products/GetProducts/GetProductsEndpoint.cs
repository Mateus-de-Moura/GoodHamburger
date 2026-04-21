using FastEndpoints;
using GoodHamburger.Application.Operations.GetHamburgers;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Products.GetProducts
{
    public class GetProductsEndpoint(IMediator _mediator) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetProductsCommand(), ct);

            if (result.IsSuccess)
               await Send.OkAsync(result.Value, ct);
            else
                await Send.OkAsync(result.Errors.First(), ct);        
        }
    }
}
