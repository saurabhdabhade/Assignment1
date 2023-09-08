using LibraryClass.Models.DTO;
using MVCApplication1.Models.ViewModel.Services.Iservice;
using MVCApplication1.Services;
using System.Configuration;

namespace MVCApplication1.Models.ViewModel.Services
{
    public class RegisterService : Service, IRegisterService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor context;
        private string registerUrl;
        public RegisterService(IHttpClientFactory clientFactory, IConfiguration configuration, IHttpContextAccessor context) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            this.context = context;
            registerUrl = configuration.GetValue<string>("ServiceUrls:Admin");
        }

        public Task<T> DeleteAsync<T>(int RegisterID)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Delete",
                Url = registerUrl + "RegisterController/" + RegisterID
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = registerUrl + "RegisterController"
            });
        }

        public Task<T> GetAsync<T>(int RegisterID)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = registerUrl + "RegisterController/" + RegisterID
            });
        }

        public Task<T> Login<T>(LoginViewModel loginRequest)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = loginRequest,
                Url = registerUrl + "RegisterController/logins"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterViewModel userVM)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = userVM,
                Url = registerUrl + "RegisterController"
            });
        }

        public Task<T> Token_Call<T>(LoginRequest loginRequest)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = loginRequest,
                Url = registerUrl + "CustomerController/login"
            });
        }

        public Task<T> UpdateAsync<T>(RegisterViewModel Entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Put",
                Data = Entity,
                Url = registerUrl + "RegisterController"
            });
        }
    }
}
