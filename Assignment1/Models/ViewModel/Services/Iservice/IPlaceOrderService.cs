using LibraryClass.Models;
using LibraryClass.Models.DTO;
using MVCApplication1.Models.ViewModel;

namespace MVCApplication1.Services.Iservice
{
    public interface IPlaceOrderService
    {
        Task<T> CreateAsync<T>(PlaceOrderViewModel Entity);
        Task<T> DeleteAsync<T>(int OrderID);
        Task<T> UpdateAsync<T>(PlaceOrderViewModel Entity);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int orderID);
    }
}
