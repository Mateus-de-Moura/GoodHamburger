using Ardalis.Result;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.DeleteOrder
{
    public class DeleteOrderCommand(Guid id) : IRequest<Result<string>>
    {
        public Guid Id { get; set; } = id;
    }
}
