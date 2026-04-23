namespace GoodHamburger.Domain.Entity
{
    public class OrderItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
