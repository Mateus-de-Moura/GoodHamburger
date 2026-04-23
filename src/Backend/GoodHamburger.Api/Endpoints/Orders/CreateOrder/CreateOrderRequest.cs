namespace GoodHamburger.Api.Endpoints.Orders.CreateOrder
{
    public class CreateOrderRequest
    {
        public List<Guid> ProductIds { get; set; } = [];
    }
}
