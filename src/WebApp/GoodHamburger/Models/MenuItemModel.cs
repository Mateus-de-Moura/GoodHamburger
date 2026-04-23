namespace GoodHamburger.Models
{
    public class MenuItemModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
