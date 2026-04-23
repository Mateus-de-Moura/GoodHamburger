using FastEndpoints;

namespace GoodHamburger.Api.Endpoints.Orders.GetOrderById
{
    public class GetOrderByIdRequest
    {
        [RouteParam, BindFrom("id")]
        public Guid Id { get; set; }
    }
}
