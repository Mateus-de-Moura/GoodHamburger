namespace GoodHamburger.Api.Endpoints.Products.GetPagedProducts
{
    public class GetPagedProductsRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
