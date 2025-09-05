using HelloApi.Models;

namespace HelloApi.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail> AddOrderDetailAsync(OrderDetail detail);
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
        Task<OrderDetail?> GetOrderDetailByIdAsync(int id);
        Task<OrderDetail?> UpdateOrderDetailAsync(OrderDetail detail);
        Task<bool> DeleteOrderDetailAsync(int id);
    }
}
