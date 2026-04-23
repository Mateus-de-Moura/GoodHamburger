using FastEndpoints;
using GoodHamburger.Application.Operations.Categories.GetCategories;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Categories.GetCategories
{
    public class GetCategoriesEndpoint(IMediator mediator) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("/categories");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await mediator.Send(new GetCategoriesCommand(), ct);

            if (result.IsSuccess)
            {
                await Send.OkAsync(result.Value, ct);
                return;
            }

            await Send.ResponseAsync(new { errors = result.Errors }, StatusCodes.Status404NotFound, ct);
        }
    }
}
