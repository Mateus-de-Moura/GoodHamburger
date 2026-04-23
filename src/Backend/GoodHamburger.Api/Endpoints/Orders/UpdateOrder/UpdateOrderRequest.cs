namespace GoodHamburger.Api.Endpoints.Orders.UpdateOrder
{
    public class UpdateOrderRequest
    {
        public Guid Id { get; set; }
        public List<Guid> ProductIds { get; set; } = [];
    }
}
