using LibraryClass.Models.DTO;
using MVCApplication1.Models.ViewModel;

namespace MVCApplication1.Services.Iservice
{
    public interface ICustomerService
    {
        //Task<T> CreateAsync<T>(CustomerViewModel Entity);
        Task<T> DeleteAsync<T>(string cust_Email);
        Task<T> UpdateAsync<T>(CustomerViewModel Entity);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(string cust_Email);
        Task<T> Login<T>(LoginRequest loginRequestDTO);
        Task CreateAsync1<T>(List<CustomerViewModel> authorsArray);
    }
}
