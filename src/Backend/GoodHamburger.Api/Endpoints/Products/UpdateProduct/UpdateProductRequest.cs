namespace GoodHamburger.Api.Endpoints.Products.UpdateProduct
{
    public class UpdateProductRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        public Guid CategoryId { get; set; }
    }
}
