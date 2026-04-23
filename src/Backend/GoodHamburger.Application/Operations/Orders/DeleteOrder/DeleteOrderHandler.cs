using Ardalis.Result;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.DeleteOrder
{
    public class DeleteOrderHandler(IOrderRepository orderRepository) : IRequestHandler<DeleteOrderCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result.Error("Pedido inválido: informe um id válido.");

            var deleted = await orderRepository.DeleteOrder(request.Id, cancellationToken);

            return deleted.IsSuccess
                ? Result.Success("Pedido removido com sucesso.")
                : Result.Error(string.Join(" | ", deleted.Errors));
        }
    }
}
