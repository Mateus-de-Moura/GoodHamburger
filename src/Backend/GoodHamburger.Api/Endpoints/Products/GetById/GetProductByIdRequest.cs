using FastEndpoints;

namespace GoodHamburger.Api.Endpoints.Products.GetById
{
    public class GetProductByIdRequest
    {
        [RouteParam, BindFrom("id")]
        public Guid Id { get; set; }
    }
}
