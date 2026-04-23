namespace GoodHamburger.Models
{
    public class OrderPreviewModel
    {
        public decimal Subtotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemModel> Items { get; set; } = [];
    }
}
