using HelloApi.Models.DTOs;

namespace HelloApi.Services
{
    public interface IItemService
    {
        Task<ItemDto> CreateAsync(ItemCreateDto dto);
        Task<ItemDto?> UpdateAsync(ItemUpdateDto dto);
        Task<IEnumerable<ItemDto>> GetAllAsync();
        Task<ItemDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
