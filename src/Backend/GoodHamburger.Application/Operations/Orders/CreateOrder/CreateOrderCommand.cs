using Ardalis.Result;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.CreateOrder
{
    public class CreateOrderCommand : IRequest<Result<Guid>>
    {
        public List<Guid> ProductIds { get; set; } = [];
    }
}
