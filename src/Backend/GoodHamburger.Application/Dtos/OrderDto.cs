namespace GoodHamburger.Application.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemDto> Items { get; set; } = [];
    }
}
