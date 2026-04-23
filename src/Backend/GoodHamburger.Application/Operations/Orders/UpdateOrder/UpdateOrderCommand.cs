using Ardalis.Result;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public List<Guid> ProductIds { get; set; } = [];
    }
}
