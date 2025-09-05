using HelloApi.Models;
using MessageApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;
        public ItemRepository(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task<Item> AddItemAsync(Item item)
        {
            _context.Item.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Item
                .OrderBy(i => i.Id)
                .ToListAsync();
        }

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            return await _context.Item.FindAsync(id);
        }

        public async Task<Item?> UpdateItemAsync(Item item)
        {
            var existing = await _context.Item.FindAsync(item.Id);
            if (existing == null) return null;

            existing.Name = item.Name;
            existing.Price = item.Price;
            existing.UpdatedBy = item.UpdatedBy;
            existing.UpdatedAt = item.UpdatedAt;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var entity = await _context.Item.FindAsync(id);
            if (entity == null) return false;

            _context.Item.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

