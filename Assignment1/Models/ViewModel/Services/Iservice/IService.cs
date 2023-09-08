using LibraryClass.Models;
using MVCApplication1.Models;

namespace MVCApplication1.Services.Iservice
{
    public interface IService
    {
        APIResponse responseMode { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
