using HelloApi.Models;
using HelloApi.Models.DTOs;
using HelloApi.Repositories;
using MessageApi.Repositories;

namespace HelloApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orders;
        private readonly IOrderDetailRepository _details;
        private readonly IItemRepository _items;
        private readonly IPersonRepository _persons;

        public OrderService(
            IOrderRepository orders,
            IOrderDetailRepository details,
            IItemRepository items,
            IPersonRepository persons)
        {
            _orders = orders;
            _details = details;
            _items = items;
            _persons = persons;
        }

        public async Task<OrderDto> CreateAsync(OrderCreateDto dto)
        {
            
            var person = await _persons.GetPersonByIdAsync(dto.PersonId);
            if (person == null) throw new InvalidOperationException($"Person {dto.PersonId} no existe.");

            if (dto.Details.Count == 0)
                throw new InvalidOperationException("La orden debe contener al menos un detalle.");

            
            var order = new Order
            {
                PersonId = dto.PersonId,
                Number = dto.Number,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            
            order.OrderDetails = new List<OrderDetail>();
            
            foreach (var d in dto.Details)
            {

                var item = await _items.GetItemByIdAsync(d.ItemId)
                           ?? throw new InvalidOperationException($"Item {d.ItemId} no existe.");

                var price = d.Price ?? item.Price; 
                var total = price * d.Quantity;

                order.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    ItemId = d.ItemId,
                    Quantity = d.Quantity,
                    Price = price,
                    Total = total,
                    CreatedBy = dto.CreatedBy,
                    CreatedAt = DateTime.UtcNow
                });
            }

            
            var saved = await _orders.AddOrderAsync(order);

            
            var full = await _orders.GetOrderByIdAsync(saved.Id) ?? saved;
            return ToDto(full);
        }

        public async Task<OrderDto?> UpdateAsync(OrderUpdateDto dto)
        {
            var existing = await _orders.GetOrderByIdAsync(dto.Id);
            if (existing == null) return null;

            // Validar persona
            var person = await _persons.GetPersonByIdAsync(dto.PersonId);
            if (person == null) throw new InvalidOperationException($"Person {dto.PersonId} no existe.");

            existing.PersonId = dto.PersonId;
            existing.Number = dto.Number;
            existing.UpdatedBy = dto.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _orders.UpdateOrderAsync(existing);
            if (updated == null) return null;

            var full = await _orders.GetOrderByIdAsync(updated.Id) ?? updated;
            return ToDto(full);
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var list = await _orders.GetAllOrdersAsync();
            return list.Select(ToDto);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var e = await _orders.GetOrderByIdAsync(id);
            return e is null ? null : ToDto(e);
        }

        public async Task<bool> DeleteAsync(int id) => await _orders.DeleteOrderAsync(id);

        private static OrderDto ToDto(Order e)
        {
            var dto = new OrderDto
            {
                Id = e.Id,
                PersonId = e.PersonId,
                Number = e.Number,
                CreatedBy = e.CreatedBy,
                CreatedAt = e.CreatedAt,
                UpdatedBy = e.UpdatedBy,
                UpdatedAt = e.UpdatedAt,
            };

            if (e.OrderDetails != null)
            {
                dto.Details = e.OrderDetails
                    .Select(d => new OrderDetailDto
                    {
                        Id = d.Id,
                        ItemId = d.ItemId,
                        ItemName = d.Item?.Name ?? string.Empty,
                        Quantity = d.Quantity,
                        Price = d.Price,
                        Total = d.Total
                    })
                    .ToList();
            }
            return dto;
        }
    }
}
