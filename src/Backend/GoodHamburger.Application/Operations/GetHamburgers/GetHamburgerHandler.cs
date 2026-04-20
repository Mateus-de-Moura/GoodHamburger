using AutoMapper;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Entity;
using MediatR;

namespace GoodHamburger.Application.Operations.GetHamburgers
{
    public class GetHamburgerHandler(IMapper _mapper) : IRequestHandler<GetHamburgersCommand, List<ProductDto>>
    {
        public Task<List<ProductDto>> Handle(GetHamburgersCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Description = "Classic Burger",
                Price = 25.90m,
                ImageBytes = []
            };

            var products = new List<Product> { product };

            var result = _mapper.Map<List<ProductDto>>(products);
            return Task.FromResult(result);
        }
    }
}
