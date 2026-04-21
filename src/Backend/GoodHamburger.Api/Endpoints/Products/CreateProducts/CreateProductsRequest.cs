namespace GoodHamburger.Api.Endpoints.Products.CreateProducts
{
    public record CreateProductsRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        public Guid CategoryId { get; set; }
    }

}
