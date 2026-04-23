using FastEndpoints;
using GoodHamburger.Application.Operations.Orders.GetOrderById;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Orders.GetOrderById
{
    public class GetOrderByIdEndpoint(IMediator mediator) : Endpoint<GetOrderByIdRequest>
    {
        public override void Configure()
        {
            Get("/orders/{id:guid}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetOrderByIdRequest req, CancellationToken ct)
        {
            var result = await mediator.Send(new GetOrderByIdCommand { Id = req.Id }, ct);

            if (result.IsSuccess)
            {
                await Send.OkAsync(result.Value, ct);
                return;
            }

            await Send.ResponseAsync(new { errors = result.Errors }, StatusCodes.Status404NotFound, ct);
        }
    }
}

