using FastEndpoints;
using GoodHamburger.Application.Operations.Orders.GetOrders;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Orders.GetOrders
{
    public class GetOrdersEndpoint(IMediator mediator) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("/orders");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await mediator.Send(new GetOrdersCommand(), ct);

            if (result.IsSuccess)
            {
                await Send.OkAsync(result.Value, ct);
                return;
            }

            await Send.ResponseAsync(new { errors = result.Errors }, StatusCodes.Status404NotFound, ct);
        }
    }
}

