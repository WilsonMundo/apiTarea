using HelloApi.Models.DTOs;

namespace HelloApi.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(OrderCreateDto dto);
        Task<OrderDto?> UpdateAsync(OrderUpdateDto dto);
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
