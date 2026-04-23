using FastEndpoints;
using GoodHamburger.Application.Operations.Products.CreateProducts;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Products.CreateProducts
{
    public class CreateProductsEndpoint(IMediator mediator, AutoMapper.IMapper mapper) : Endpoint<CreateProductsRequest>
    {
        public override void Configure()
        {
            Post("/products");
            AllowAnonymous();
            AllowFileUploads();
        }
        public override async Task HandleAsync(CreateProductsRequest req, CancellationToken ct)
        {
            var mapped = mapper.Map<CreateProductsCommand>(req);

            var result = await mediator.Send(mapped, ct);

            if (result.IsSuccess)
                await Send.CreatedAtAsync(result.Value, ct);
            else
                await Send.OkAsync(result.Errors.First().ToString(), ct);
        }
    }
}
