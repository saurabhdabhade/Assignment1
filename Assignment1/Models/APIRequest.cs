namespace MVCApplication1.Models
{
    public class APIRequest
    {
        public string ApiType { get; set; }
        public string Url { get; set; }
        public Object Data { get; set; }
        public string Token { get; set; }
    }
}
