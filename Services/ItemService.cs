using HelloApi.Models;
using HelloApi.Models.DTOs;
using HelloApi.Repositories;

namespace HelloApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _items;

        public ItemService(IItemRepository items)
        {
            _items = items;
        }

        public async Task<ItemDto> CreateAsync(ItemCreateDto dto)
        {
            var entity = new Item
            {
                Name = dto.Name,
                Price = dto.Price,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            var saved = await _items.AddItemAsync(entity);
            return ToDto(saved);
        }

        public async Task<ItemDto?> UpdateAsync(ItemUpdateDto dto)
        {
            var existing = await _items.GetItemByIdAsync(dto.Id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.UpdatedBy = dto.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _items.UpdateItemAsync(existing);
            return updated is null ? null : ToDto(updated);
        }

        public async Task<IEnumerable<ItemDto>> GetAllAsync()
        {
            var list = await _items.GetAllItemsAsync();
            return list.Select(ToDto);
        }

        public async Task<ItemDto?> GetByIdAsync(int id)
        {
            var e = await _items.GetItemByIdAsync(id);
            return e is null ? null : ToDto(e);
        }

        public async Task<bool> DeleteAsync(int id) => await _items.DeleteItemAsync(id);

        private static ItemDto ToDto(Item e) => new()
        {
            Id = e.Id,
            Name = e.Name,
            Price = e.Price,
            CreatedBy = e.CreatedBy,
            CreatedAt = e.CreatedAt,
            UpdatedBy = e.UpdatedBy,
            UpdatedAt = e.UpdatedAt
        };
    }
}
