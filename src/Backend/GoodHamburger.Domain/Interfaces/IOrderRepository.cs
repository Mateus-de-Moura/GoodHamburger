using Ardalis.Result;
using GoodHamburger.Domain.Entity;

namespace GoodHamburger.Domain.Interfaces
{
    public interface IOrderRepository
    {
        public Task<Result<List<Order>>> GetAllOrders(CancellationToken ct);
        public Task<Result<Order>> GetOrderById(Guid id, CancellationToken ct);
        public Task<Result<Order>> CreateOrder(Order order, CancellationToken ct);
        public Task<Result<bool>> UpdateOrder(Order order, CancellationToken ct);
        public Task<Result<bool>> DeleteOrder(Guid id, CancellationToken ct);
    }
}
