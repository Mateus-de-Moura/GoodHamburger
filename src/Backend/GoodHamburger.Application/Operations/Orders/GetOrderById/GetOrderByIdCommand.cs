using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.GetOrderById
{
    public class GetOrderByIdCommand : IRequest<Result<OrderDto>>
    {
        public Guid Id { get; set; }
    }
}
