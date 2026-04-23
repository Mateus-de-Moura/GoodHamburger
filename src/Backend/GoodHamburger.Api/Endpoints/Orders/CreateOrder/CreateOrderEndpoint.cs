using FastEndpoints;
using GoodHamburger.Application.Operations.Orders.CreateOrder;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Orders.CreateOrder
{
    public class CreateOrderEndpoint(IMediator mediator, AutoMapper.IMapper mapper) : Endpoint<CreateOrderRequest>
    {
        public override void Configure()
        {
            Post("/orders");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateOrderRequest req, CancellationToken ct)
        {
            var command = mapper.Map<CreateOrderCommand>(req);
            var result = await mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                await Send.ResponseAsync(new { id = result.Value }, StatusCodes.Status201Created, ct);
                return;
            }

            await Send.ResponseAsync(new { errors = result.Errors }, StatusCodes.Status400BadRequest, ct);
        }
    }
}

