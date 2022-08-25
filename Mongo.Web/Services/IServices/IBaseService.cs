using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        public ResponsDto responsDto { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
