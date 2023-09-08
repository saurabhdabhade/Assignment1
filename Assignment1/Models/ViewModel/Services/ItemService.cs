using LibraryClass.Models;
using LibraryClass.Models.DTO;
using MVCApplication1.Models;
using MVCApplication1.Models.ViewModel;
using MVCApplication1.Services.Iservice;
using System.Security.Policy;

namespace MVCApplication1.Services
{
    public class ItemService : Service,IItemService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string itemUrl;
        public ItemService(IHttpClientFactory clientFactory, IConfiguration configuration, IHttpContextAccessor context) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            itemUrl = configuration.GetValue<string>("ServiceUrls:Admin");
        }

        public Task<T> CreateAsync<T>(ItemViewModel Entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = Entity,
                Url = itemUrl + "ItemController"
            }); 
        }

        public Task<T> DeleteAsync<T>(string itemName)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Delete",
                Url = itemUrl + "ItemController/" + itemName
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = itemUrl + "ItemController"
            });
        }

    public Task<T> GetAsync<T>(string itemName)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = itemUrl + "ItemController/" + itemName
            });
        }

        public Task<T> UpdateAsync<T>(ItemViewModel Entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Put",
                Data = Entity,
                Url = itemUrl + "ItemController"
            });
        }
    }
}
