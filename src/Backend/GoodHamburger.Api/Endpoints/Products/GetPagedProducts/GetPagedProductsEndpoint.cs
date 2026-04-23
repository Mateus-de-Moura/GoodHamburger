using FastEndpoints;
using GoodHamburger.Application.Operations.Products.GetPagedProducts;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Products.GetPagedProducts
{
    public class GetPagedProductsEndpoint(IMediator mediator) : Endpoint<GetPagedProductsRequest>
    {
        public override void Configure()
        {
            Get("/products/paged");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetPagedProductsRequest req, CancellationToken ct)
        {
            var command = new GetPagedProductsCommand
            {
                PageNumber = req.PageNumber,
                PageSize = req.PageSize
            };

            var result = await mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                await Send.OkAsync(result.Value, ct);
                return;
            }

            await Send.ResponseAsync(new { errors = result.Errors }, StatusCodes.Status400BadRequest, ct);
        }
    }
}
