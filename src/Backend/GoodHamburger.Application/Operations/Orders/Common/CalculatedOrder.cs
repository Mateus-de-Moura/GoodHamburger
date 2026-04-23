namespace GoodHamburger.Application.Operations.Orders.Common
{
    public class CalculatedOrderItem
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public MenuItemType Type { get; set; }
        public string Category { get; set; } = string.Empty;
    }

    public class CalculatedOrder
    {
        public required IReadOnlyCollection<CalculatedOrderItem> Items { get; init; }
        public decimal Subtotal { get; init; }
        public decimal DiscountPercentage { get; init; }
        public decimal DiscountAmount { get; init; }
        public decimal Total { get; init; }
    }
}
