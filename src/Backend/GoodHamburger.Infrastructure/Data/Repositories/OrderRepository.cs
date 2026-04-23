using Ardalis.Result;
using GoodHamburger.Domain.Entity;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data.Repositories
{
    public class OrderRepository(GoodHamburgerDbContext context) : IOrderRepository
    {
        public async Task<Result<Order>> CreateOrder(Order order, CancellationToken ct)
        {
            await context.Orders.AddAsync(order, ct);

            var rowsAffected = await context.SaveChangesAsync(ct);

            return rowsAffected > 0
                ? Result<Order>.Success(order)
                : Result<Order>.Error("Ocorreu um erro ao cadastrar o pedido.");
        }

        public async Task<Result<bool>> DeleteOrder(Guid id, CancellationToken ct)
        {
            var existingOrder = await context.Orders
                .Where(o => o.Active)
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id, ct);

            if (existingOrder is null)
                return Result<bool>.NotFound("Pedido não encontrado.");

            existingOrder.Active = false;
            existingOrder.UpdatedAt = DateTime.UtcNow;

            foreach (var item in existingOrder.Items)
            {
                item.Active = false;
                item.UpdatedAt = DateTime.UtcNow;
            }

            var rowsAffected = await context.SaveChangesAsync(ct);

            return rowsAffected > 0
                ? Result.Success(true)
                : Result.Error("Ocorreu um erro ao remover o pedido.");
        }

        public async Task<Result<List<Order>>> GetAllOrders(CancellationToken ct)
        {
            var orders = await context.Orders
                .AsNoTracking()
                .Where(o => o.Active)
                .Include(o => o.Items.Where(i => i.Active))
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(ct);

            return orders.Count > 0
                ? Result<List<Order>>.Success(orders)
                : Result<List<Order>>.NotFound("Nenhum pedido cadastrado.");
        }

        public async Task<Result<Order>> GetOrderById(Guid id, CancellationToken ct)
        {
            var order = await context.Orders
                .AsNoTracking()
                .Where(o => o.Active)
                .Include(o => o.Items.Where(i => i.Active))
                .FirstOrDefaultAsync(o => o.Id == id, ct);

            return order is not null
                ? Result<Order>.Success(order)
                : Result<Order>.NotFound("Pedido não encontrado.");
        }

        public async Task<Result<bool>> UpdateOrder(Order order, CancellationToken ct)
        {
            var existingOrder = await context.Orders
                .Where(o => o.Active)
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == order.Id, ct);

            if (existingOrder is null)
                return Result<bool>.NotFound("Pedido não encontrado.");

            context.OrderItems.RemoveRange(existingOrder.Items);

            existingOrder.Subtotal = order.Subtotal;
            existingOrder.DiscountPercentage = order.DiscountPercentage;
            existingOrder.DiscountAmount = order.DiscountAmount;
            existingOrder.Total = order.Total;
            existingOrder.UpdatedAt = DateTime.UtcNow;

            var newItems = order.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Name = i.Name,
                Type = i.Type,
                UnitPrice = i.UnitPrice,
                OrderId = existingOrder.Id
            }).ToList();

            existingOrder.Items = newItems;
            await context.OrderItems.AddRangeAsync(newItems, ct);

            var rowsAffected = await context.SaveChangesAsync(ct);

            return rowsAffected > 0
                ? Result.Success(true)
                : Result.Error("Ocorreu um erro ao atualizar o pedido.");
        }
    }
}
