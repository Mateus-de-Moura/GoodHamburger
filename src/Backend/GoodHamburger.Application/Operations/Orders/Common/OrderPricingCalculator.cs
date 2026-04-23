using Ardalis.Result;
using GoodHamburger.Domain.Entity;

namespace GoodHamburger.Application.Operations.Orders.Common
{
    public static class OrderPricingCalculator
    {
        public static Result<CalculatedOrder> Calculate(IEnumerable<Product> products)
        {
            var selectedProducts = (products ?? Enumerable.Empty<Product>())
                .Where(p => p is not null)
                .ToList();

            if (selectedProducts.Count == 0)
                return Result<CalculatedOrder>.Error("Pedido inválido: informe ao menos um produto.");

            var duplicatedProducts = selectedProducts
                .GroupBy(p => p.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicatedProducts.Count > 0)
                return Result<CalculatedOrder>.Error($"Produtos duplicados não são permitidos no pedido: {string.Join(", ", duplicatedProducts)}.");

            var items = new List<CalculatedOrderItem>();
            var unmappedProducts = new List<string>();

            foreach (var product in selectedProducts)
            {
                if (!OrderProductClassifier.TryGetType(product, out var itemType))
                {
                    var categoryName = product.Category?.Description ?? "Sem categoria";
                    unmappedProducts.Add($"{product.Name} ({categoryName})");
                    continue;
                }

                items.Add(new CalculatedOrderItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Type = itemType,
                    Category = product.Category?.Description ?? string.Empty
                });
            }

            if (unmappedProducts.Count > 0)
                return Result<CalculatedOrder>.Error(
                    $"Não foi possível classificar os produtos para aplicar desconto: {string.Join(", ", unmappedProducts)}. " +
                    "Use categorias de Lanche, Acompanhamento ou Bebida (ou nomes compatíveis).");

            if (items.Count(i => i.Type == MenuItemType.Sandwich) > 1)
                return Result<CalculatedOrder>.Error("Cada pedido pode conter apenas um lanche.");

            if (items.Count(i => i.Type == MenuItemType.Side) > 1)
                return Result<CalculatedOrder>.Error("Cada pedido pode conter apenas um acompanhamento.");

            if (items.Count(i => i.Type == MenuItemType.Drink) > 1)
                return Result<CalculatedOrder>.Error("Cada pedido pode conter apenas uma bebida.");

            var subtotal = items.Sum(i => i.Price);

            var hasSandwich = items.Any(i => i.Type == MenuItemType.Sandwich);
            var hasSide = items.Any(i => i.Type == MenuItemType.Side);
            var hasDrink = items.Any(i => i.Type == MenuItemType.Drink);

            var discountPercentage = hasSandwich && hasSide && hasDrink
                ? 0.20m
                : hasSandwich && hasDrink
                    ? 0.15m
                    : hasSandwich && hasSide
                        ? 0.10m
                        : 0.00m;

            var discountAmount = decimal.Round(subtotal * discountPercentage, 2, MidpointRounding.AwayFromZero);
            var total = subtotal - discountAmount;

            return Result<CalculatedOrder>.Success(new CalculatedOrder
            {
                Items = items,
                Subtotal = subtotal,
                DiscountPercentage = discountPercentage,
                DiscountAmount = discountAmount,
                Total = total
            });
        }
    }
}
