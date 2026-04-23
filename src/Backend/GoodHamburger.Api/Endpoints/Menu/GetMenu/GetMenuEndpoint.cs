using FastEndpoints;
using GoodHamburger.Application.Operations.Orders.GetMenu;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Menu.GetMenu
{
    public class GetMenuEndpoint(IMediator mediator) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("/menu");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await mediator.Send(new GetMenuCommand(), ct);

            if (result.IsSuccess)
            {
                await Send.OkAsync(result.Value, ct);
                return;
            }

            await Send.ResponseAsync(new { errors = result.Errors }, StatusCodes.Status400BadRequest, ct);
        }
    }
}

