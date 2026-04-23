using Ardalis.Result;
using AutoMapper;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.GetOrderById
{
    public class GetOrderByIdHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrderByIdCommand, Result<OrderDto>>
    {
        public async Task<Result<OrderDto>> Handle(GetOrderByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result.Error("Pedido inválido: informe um id válido.");

            var order = await orderRepository.GetOrderById(request.Id, cancellationToken);

            if (order.IsSuccess)
            {
                var mapped = mapper.Map<OrderDto>(order.Value);
                return Result.Success(mapped);
            }

            return Result.Error(order.Errors.First());
        }
    }
}
