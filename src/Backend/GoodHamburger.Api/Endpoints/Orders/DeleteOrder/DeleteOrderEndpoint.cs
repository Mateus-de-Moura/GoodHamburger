using FastEndpoints;
using GoodHamburger.Application.Operations.Orders.DeleteOrder;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Orders.DeleteOrder
{
    public class DeleteOrderEndpoint(IMediator mediator) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Delete("/orders/{id:guid}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var result = await mediator.Send(new DeleteOrderCommand(id), ct);

            if (result.IsSuccess)
            {
                await Send.OkAsync(new { message = result.Value }, ct);
                return;
            }

            var statusCode = result.Errors.Any(e => e.Contains("não encontrado", StringComparison.OrdinalIgnoreCase))
                ? StatusCodes.Status404NotFound
                : StatusCodes.Status400BadRequest;

            await Send.ResponseAsync(new { errors = result.Errors }, statusCode, ct);
        }
    }
}

