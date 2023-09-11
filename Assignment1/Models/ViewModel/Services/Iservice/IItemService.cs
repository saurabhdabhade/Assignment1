
using MVCApplication1.Models.ViewModel;

namespace MVCApplication1.Services.Iservice
{
    public interface IItemService
    {
        Task<T> CreateAsync<T>(ItemViewModel Entity);
        Task<T> DeleteAsync<T>(string itemName);
        Task<T> UpdateAsync<T>(ItemViewModel Entity);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(string itemName);
        Task CreateAsync1<T>(List<ItemViewModel> authorsArray);

    }
}
