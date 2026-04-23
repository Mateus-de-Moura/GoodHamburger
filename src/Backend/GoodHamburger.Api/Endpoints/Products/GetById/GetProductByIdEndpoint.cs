using FastEndpoints;
using GoodHamburger.Application.Operations.Products.GetById;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Products.GetById
{
    public class GetProductByIdEndpoint(IMediator mediator) : Endpoint<GetProductByIdRequest>
    {
        public override void Configure()
        {
            Get("/products/{id:guid}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetProductByIdRequest req, CancellationToken ct)
        {
            var result = await mediator.Send(new GetProductByIdCommand { Id = req.Id }, ct);

            if (result.IsSuccess)
                await Send.OkAsync(result.Value, ct);
            else
                await Send.NotFoundAsync(ct);
        }
    }
}

