using GoodHamburger.Application.Dtos;
using MediatR;

namespace GoodHamburger.Application.Operations.GetHamburgers
{
    public class GetHamburgersCommand : IRequest<List<ProductDto>>
    {
    }
}
