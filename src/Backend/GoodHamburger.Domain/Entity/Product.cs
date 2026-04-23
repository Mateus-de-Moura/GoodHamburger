#nullable enable

namespace GoodHamburger.Domain.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public Guid ProductImageId { get; set; }
        public Category Category { get; set; } = null!;
        public ProductImage? ProductImage { get; set; }
    }
}
