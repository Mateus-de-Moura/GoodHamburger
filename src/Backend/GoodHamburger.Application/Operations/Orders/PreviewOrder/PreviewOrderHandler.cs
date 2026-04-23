using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Application.Operations.Orders.Common;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.PreviewOrder
{
    public class PreviewOrderHandler(IProductRepository productRepository)
        : IRequestHandler<PreviewOrderCommand, Result<OrderPreviewDto>>
    {
        public async Task<Result<OrderPreviewDto>> Handle(PreviewOrderCommand request, CancellationToken cancellationToken)
        {
            var productIds = request.ProductIds ?? [];

            if (productIds.Count == 0)
                return Result<OrderPreviewDto>.Error("Selecione ao menos um item para pré-visualizar o pedido.");

            if (productIds.Any(id => id == Guid.Empty))
                return Result<OrderPreviewDto>.Error("Existem ids de produto inválidos na seleção.");

            if (productIds.GroupBy(id => id).Any(g => g.Count() > 1))
                return Result<OrderPreviewDto>.Error("Produtos duplicados não são permitidos no pedido.");

            var productsResult = await productRepository.GetProductsByIds(productIds, cancellationToken);

            if (!productsResult.IsSuccess)
                return Result<OrderPreviewDto>.Error(string.Join(" | ", productsResult.Errors));

            var calculated = OrderPricingCalculator.Calculate(productsResult.Value);

            if (!calculated.IsSuccess)
                return Result<OrderPreviewDto>.Error(string.Join(" | ", calculated.Errors));

            var preview = new OrderPreviewDto
            {
                Subtotal = calculated.Value.Subtotal,
                DiscountPercentage = calculated.Value.DiscountPercentage,
                DiscountAmount = calculated.Value.DiscountAmount,
                Total = calculated.Value.Total,
                Items = calculated.Value.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Name = i.Name,
                    Type = i.Type.ToString(),
                    UnitPrice = i.Price
                }).ToList()
            };

            return Result<OrderPreviewDto>.Success(preview);
        }
    }
}
