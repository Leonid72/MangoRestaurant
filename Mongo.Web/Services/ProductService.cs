using Mango.Web.Models;
using Mango.Web.Services.IServices;
using System.Data.SqlTypes;

namespace Mango.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        //private readonly IHttpClientFactory _httpClient;
        public HttpClient _httpClient { get; set; }
        public ProductService(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> CreataProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = productDto,
                Url = SD.ProuctAPIBase + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Delete,
                Url = SD.ProuctAPIBase + "/api/products" +id,
                AccessToken = ""
            });
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Get,
                Url = SD.ProuctAPIBase + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Get,
                Url = SD.ProuctAPIBase + "/api/products" + id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Put,
                Data = productDto,
                Url = SD.ProuctAPIBase + "/api/products",
                AccessToken = ""
            });
        }
    }
}
