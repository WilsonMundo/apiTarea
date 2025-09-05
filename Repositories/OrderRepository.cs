using HelloApi.Models;
using MessageApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repositories
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Order> AddOrderAsync(Order order)
        {
            
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Person)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                .OrderBy(o => o.Id)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Person)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> UpdateOrderAsync(Order order)
        {
            var existing = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            if (existing == null) return null;

            
            existing.PersonId = order.PersonId;
            existing.Number = order.Number;
            existing.UpdatedBy = order.UpdatedBy;
            existing.UpdatedAt = order.UpdatedAt;

            
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var entity = await _context.Orders.FindAsync(id);
            if (entity == null) return false;

            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
