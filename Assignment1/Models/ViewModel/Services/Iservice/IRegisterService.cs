using LibraryClass.Models.DTO;

namespace MVCApplication1.Models.ViewModel.Services.Iservice
{
    public interface IRegisterService
    {
        Task<T> RegisterAsync<T>(RegisterViewModel userVM);
        Task<T> GetAllAsync<T>();
        Task<T> DeleteAsync<T>(int RegisterID);
        Task<T> UpdateAsync<T>(RegisterViewModel Entity);
        Task<T> GetAsync<T>(int RegisterID);
        Task<T> Login<T>(LoginViewModel loginRequest);
        Task<T> Token_Call<T>( LoginRequest loginRequest);
    }
}
