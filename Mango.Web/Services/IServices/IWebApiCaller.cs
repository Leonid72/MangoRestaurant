using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IWebApiCaller 
    {
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
