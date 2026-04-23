namespace GoodHamburger.Models
{
    public class OrderItemModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
