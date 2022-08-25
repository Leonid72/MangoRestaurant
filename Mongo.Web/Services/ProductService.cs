using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _httpClient;
        public ProductService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<T> CreataProductAsync<T>(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteProductAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAllProductsAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetProductByIdAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}
