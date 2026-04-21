using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GoodHamburger.Application.Operations.CreateProducts
{
    public class CreateProductsCommand : IRequest<Result<string>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        public Guid CategoryId { get; set; }
    }
}
