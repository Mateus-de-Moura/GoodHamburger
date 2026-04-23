using Ardalis.Result;
using AutoMapper;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.GetOrders
{
    public class GetOrdersHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersCommand, Result<List<OrderDto>>>
    {
        public async Task<Result<List<OrderDto>>> Handle(GetOrdersCommand request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetAllOrders(cancellationToken);

            if (orders.IsSuccess)
            {
                var mapped = mapper.Map<List<OrderDto>>(orders.Value);
                return Result.Success(mapped);
            }

            return Result.Error(orders.Errors.First());
        }
    }
}
