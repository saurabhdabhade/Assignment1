using LibraryClass.Models.DTO;
using MVCApplication1.Models;
using MVCApplication1.Models.ViewModel;
using MVCApplication1.Services.Iservice;
using System.Security.Policy;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCApplication1.Services
{
    public class CustomerService : Service , ICustomerService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor context;
        private string customerUrl;    

        public CustomerService(IHttpClientFactory clientFactory, IConfiguration configuration, IHttpContextAccessor context) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            this.context = context;
            customerUrl = configuration.GetValue<string>("ServiceUrls:Admin");
        }
     
       /* public Task<T> CreateAsync<T>(CustomerViewModel Entity)
        {

            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = Entity,
                Url = customerUrl + "CustomerController"
            });
        }*/

        public Task<T> DeleteAsync<T>(string cust_Email)
        {
           
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Delete",
                Url = customerUrl + "CustomerController/" + cust_Email
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = customerUrl + "CustomerController"
            });
        }

        public Task<T> GetAsync<T>(string cust_Email)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = customerUrl + "CustomerController/" + cust_Email
            });
        }

        /* public Task<T> GetAsync<T>(string cust_Email)
         {
             return SendAsync<T>(new APIRequest()
             {
                 ApiType = "Get",
                 Url = customerUrl + "CustomerController/" + cust_Email
             });
         }*/

        public Task<T> Login<T>(LoginRequest loginRequestDTO)
        {  
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = loginRequestDTO,
                Url = customerUrl + "CustomerController/login"
            });
        }

        public Task<T> UpdateAsync<T>(CustomerViewModel Entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Put",
                Data = Entity,
                Url = customerUrl + "CustomerController",
            });
        }

        Task ICustomerService.CreateAsync1<T>(List<CustomerViewModel> authorsArray)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = authorsArray,
                Url = customerUrl + "CustomerController"
            });
        }
    }
}
