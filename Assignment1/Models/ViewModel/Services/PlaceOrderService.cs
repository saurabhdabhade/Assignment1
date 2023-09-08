using AutoMapper.Internal;
using Humanizer;
using LibraryClass.Models;
using LibraryClass.Models.DTO;
using MVCApplication1.Models;
using MVCApplication1.Models.ViewModel;
using MVCApplication1.Services.Iservice;

namespace MVCApplication1.Services
{
    public class PlaceOrderService : Service,IPlaceOrderService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string placeOrderUrl;
        public PlaceOrderService(IHttpClientFactory clientFactory, IConfiguration configuration, IHttpContextAccessor context) :base(clientFactory)
        {
            _clientFactory = clientFactory;
            placeOrderUrl = configuration.GetValue<string>("ServiceUrls:Admin");
        }
        public Task<T> CreateAsync<T>(PlaceOrderViewModel Entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = Entity,
                Url = placeOrderUrl + "PlaceOrderController"
                
            });
        }

        public Task<T> DeleteAsync<T>(int OrderID)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Delete",
                Url = placeOrderUrl + "PlaceOrderController/" + OrderID
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = placeOrderUrl + "PlaceOrderController"
            });
        }

        public Task<T> GetAsync<T>(int orderID)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = placeOrderUrl + "PlaceOrderController/" + orderID
            });
        }

        public Task<T> UpdateAsync<T>(PlaceOrderViewModel Entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Put",
                Data = Entity,
                Url = placeOrderUrl + "PlaceOrderController"
            });
        }
    }
}
