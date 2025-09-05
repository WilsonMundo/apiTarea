using HelloApi.Models;

namespace HelloApi.Repositories
{
    public interface IItemRepository
    {
        Task<Item> AddItemAsync(Item item);
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task<Item?> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(int id);
    }
}
