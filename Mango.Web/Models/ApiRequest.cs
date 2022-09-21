using static Mango.Web.SD;

namespace Mango.Web.Models
{
    public class ApiRequest
    {
        public HttpMethod ApiType { get; set; } = HttpMethod.Get;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
