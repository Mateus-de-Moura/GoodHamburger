using FastEndpoints;
using GoodHamburger.Application.Operations.CreateProducts;
using MediatR;

namespace GoodHamburger.Api.Endpoints.Products.CreateProducts
{
    public class CreateProductsEndpoint(IMediator mediator) : Endpoint<CreateProductsRequest>
    {
        public override void Configure()
        {
            Post("/products");
            AllowAnonymous();
            AllowFileUploads();
        }
        public override Task HandleAsync(CreateProductsRequest req, CancellationToken ct)
        {
            var result = mediator.Send(new CreateProductsCommand
            {
                Name = req.Name,
                Description = req.Description,
                Price = req.Price,
                Image = req.Image,
                CategoryId = req.CategoryId
            }, ct).Result;

            return Send.OkAsync(ct);
        }
    }
}
