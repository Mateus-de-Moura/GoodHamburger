namespace GoodHamburger.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
        public string? ImageBase64 { get; set; }
    }
}
