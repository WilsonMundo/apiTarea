using HelloApi.Models;

namespace HelloApi.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order?> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
