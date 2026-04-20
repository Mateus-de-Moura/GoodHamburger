using FastEndpoints;
using GoodHamburger.Application.Operations.GetHamburgers;
using MediatR;

namespace GoodHamburger.Api.Endpoints.GetHambugerList
{
    public class GetHamburgerEndpoint(IMediator _mediator) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetHamburgersCommand(), ct);

            await Send.OkAsync(new { Message = result }, ct);
        }
    }
}
