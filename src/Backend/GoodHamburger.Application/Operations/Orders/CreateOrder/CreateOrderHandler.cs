using Ardalis.Result;
using GoodHamburger.Application.Operations.Orders.Common;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.CreateOrder
{
    public class CreateOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository)
        : IRequestHandler<CreateOrderCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var productIds = request.ProductIds ?? [];

            if (productIds.Count == 0)
                return Result<Guid>.Error("Pedido inválido: informe ao menos um produto.");

            if (productIds.Any(id => id == Guid.Empty))
                return Result<Guid>.Error("Pedido inválido: existem ids de produto vazios.");

            var duplicatedIds = productIds
                .GroupBy(id => id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicatedIds.Count > 0)
                return Result<Guid>.Error($"Produtos duplicados no pedido: {string.Join(", ", duplicatedIds)}.");

            var productsResult = await productRepository.GetProductsByIds(productIds, cancellationToken);

            if (!productsResult.IsSuccess)
                return Result<Guid>.Error(string.Join(" | ", productsResult.Errors));

            var foundIds = productsResult.Value.Select(p => p.Id).ToHashSet();
            var missingIds = productIds.Where(id => !foundIds.Contains(id)).Distinct().ToList();

            if (missingIds.Count > 0)
                return Result<Guid>.Error($"Produtos não encontrados: {string.Join(", ", missingIds)}.");

            var calculated = OrderPricingCalculator.Calculate(productsResult.Value);

            if (!calculated.IsSuccess)
                return Result<Guid>.Error(string.Join(" | ", calculated.Errors));

            var order = new Order
            {
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

            var created = await orderRepository.CreateOrder(order, cancellationToken);

            return created.IsSuccess
                ? Result<Guid>.Success(created.Value.Id)
                : Result<Guid>.Error(string.Join(" | ", created.Errors));
        }
    }
}
