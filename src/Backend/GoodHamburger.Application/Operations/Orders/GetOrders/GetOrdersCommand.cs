using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.GetOrders
{
    public class GetOrdersCommand : IRequest<Result<List<OrderDto>>>
    {
    }
}
