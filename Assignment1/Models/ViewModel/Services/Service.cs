using LibraryClass.Models;
using MVCApplication1.Models;
using MVCApplication1.Services.Iservice;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVCApplication1.Services
{
    public class Service : IService
    {
        public IHttpClientFactory httpClient { get; set; }
        public APIResponse responseMode { get; set; }

        public Service(IHttpClientFactory httpClient)
        {
            this.responseMode = new();
            this.httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                //message.Headers.Add("Accept", "application/json");
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case "Post":
                        message.Method = HttpMethod.Post;
                        break;
                    case "Put":
                        message.Method = HttpMethod.Put;
                        break;
                    case "Delete":
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                if(apiRequest.Token != null)
                {
                    client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",apiRequest.Token);
                }

                HttpResponseMessage apiResponse = null;

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                try
                {
                    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (ApiResponse != null && (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound))
                    {
                        ApiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                        ApiResponse.IsSuccess = true;
                        var res = JsonConvert.SerializeObject(ApiResponse.Result);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }
                }
                catch (Exception e)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;
                }
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch (Exception e) 
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
