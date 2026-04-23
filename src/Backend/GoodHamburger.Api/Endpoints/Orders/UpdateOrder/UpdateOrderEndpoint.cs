using FastEndpoints;
using GoodHamburger.Application.Operations.Orders.UpdateOrder;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Orders.UpdateOrder
{
    public class UpdateOrderEndpoint(IMediator mediator, AutoMapper.IMapper mapper) : Endpoint<UpdateOrderRequest>
    {
        public override void Configure()
        {
            Put("/orders");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateOrderRequest req, CancellationToken ct)
        {
            var command = mapper.Map<UpdateOrderCommand>(req);
            var result = await mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                await Send.OkAsync(new { message = "Pedido atualizado com sucesso." }, ct);
                return;
            }

            var statusCode = result.Errors.Any(e => e.Contains("não encontrado", StringComparison.OrdinalIgnoreCase))
                ? StatusCodes.Status404NotFound
                : StatusCodes.Status400BadRequest;

            await Send.ResponseAsync(new { errors = result.Errors }, statusCode, ct);
        }
    }
}

