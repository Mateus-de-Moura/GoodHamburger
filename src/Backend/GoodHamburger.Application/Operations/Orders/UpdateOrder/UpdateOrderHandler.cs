using Ardalis.Result;
using GoodHamburger.Application.Operations.Orders.Common;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.UpdateOrder
{
    public class UpdateOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository)
        : IRequestHandler<UpdateOrderCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result.Error("Pedido inválido: informe um id válido.");

            var productIds = request.ProductIds ?? [];

            if (productIds.Count == 0)
                return Result<bool>.Error("Pedido inválido: informe ao menos um produto.");

            if (productIds.Any(id => id == Guid.Empty))
                return Result<bool>.Error("Pedido inválido: existem ids de produto vazios.");

            var duplicatedIds = productIds
                .GroupBy(id => id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicatedIds.Count > 0)
                return Result<bool>.Error($"Produtos duplicados no pedido: {string.Join(", ", duplicatedIds)}.");

            var productsResult = await productRepository.GetProductsByIds(productIds, cancellationToken);

            if (!productsResult.IsSuccess)
                return Result<bool>.Error(string.Join(" | ", productsResult.Errors));

            var foundIds = productsResult.Value.Select(p => p.Id).ToHashSet();
            var missingIds = productIds.Where(id => !foundIds.Contains(id)).Distinct().ToList();

            if (missingIds.Count > 0)
                return Result<bool>.Error($"Produtos não encontrados: {string.Join(", ", missingIds)}.");

            var calculated = OrderPricingCalculator.Calculate(productsResult.Value);

            if (!calculated.IsSuccess)
                return Result<bool>.Error(string.Join(" | ", calculated.Errors));

            var order = new Order
            {
                Id = request.Id,
                Subtotal = calculated.Value.Subtotal,
                DiscountPercentage = calculated.Value.DiscountPercentage,
                DiscountAmount = calculated.Value.DiscountAmount,
                Total = calculated.Value.Total,
                Items = calculated.Value.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Name = i.Name,
                    Type = i.Type.ToString(),
                    UnitPrice = i.Price
                }).ToList()
            };

            var updated = await orderRepository.UpdateOrder(order, cancellationToken);

            return updated.IsSuccess
                ? Result.Success(true)
                : Result<bool>.Error(string.Join(" | ", updated.Errors));
        }
    }
}
