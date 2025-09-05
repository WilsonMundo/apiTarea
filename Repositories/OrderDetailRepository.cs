using HelloApi.Models;
using MessageApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repositories
{
    public class OrderDetailRepository(AppDbContext context) : IOrderDetailRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<OrderDetail> AddOrderDetailAsync(OrderDetail detail)
        {
            _context.OrderDetails.Add(detail);
            await _context.SaveChangesAsync();
            return detail;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _context.OrderDetails
                .Include(d => d.Item)
                .Include(d => d.Order)
                .OrderBy(d => d.Id)
                .ToListAsync();
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails
                .Include(d => d.Item)
                .Include(d => d.Order)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<OrderDetail?> UpdateOrderDetailAsync(OrderDetail detail)
        {
            var existing = await _context.OrderDetails.FindAsync(detail.Id);
            if (existing == null) return null;

            existing.OrderId = detail.OrderId;
            existing.ItemId = detail.ItemId;
            existing.Quantity = detail.Quantity;
            existing.Price = detail.Price;
            existing.Total = detail.Total;
            existing.UpdatedBy = detail.UpdatedBy;
            existing.UpdatedAt = detail.UpdatedAt;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            var entity = await _context.OrderDetails.FindAsync(id);
            if (entity == null) return false;

            _context.OrderDetails.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    
}
