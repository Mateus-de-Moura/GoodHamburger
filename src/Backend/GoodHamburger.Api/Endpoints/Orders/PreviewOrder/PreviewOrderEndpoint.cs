using FastEndpoints;
using GoodHamburger.Application.Operations.Orders.PreviewOrder;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Orders.PreviewOrder
{
    public class PreviewOrderEndpoint(IMediator mediator, AutoMapper.IMapper mapper) : Endpoint<PreviewOrderRequest>
    {
        public override void Configure()
        {
            Post("/orders/preview");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PreviewOrderRequest req, CancellationToken ct)
        {
            var command = mapper.Map<PreviewOrderCommand>(req);
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
