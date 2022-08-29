using Mango.Web.Models;
using Mango.Web.Services.IServices;
using System.Data.SqlTypes;

namespace Mango.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IWebApiCaller _webApiCaller;

        public ProductService(IWebApiCaller webApiCaller) 
        {
           _webApiCaller = webApiCaller;
        }

        public  Task<T> CreataProductAsync<T>(ProductDto productDto)
        {
            return _webApiCaller.SendAsync<T>(CrateRequest(HttpMethod.Post,productDto));
        }

        public  Task<T> DeleteProductAsync<T>(int id)
        {
            return _webApiCaller.SendAsync<T>(CrateRequest(HttpMethod.Delete, $"/api/products/{id}"));
        }

        public  Task<T> GetAllProductsAsync<T>()
        {
            return _webApiCaller.SendAsync<T>(CrateRequest(HttpMethod.Get));
        }

        public  Task<T> GetProductByIdAsync<T>(int id)
        {
            return _webApiCaller.SendAsync<T>(CrateRequest(HttpMethod.Get, $"/api/products/{id}"));
        }

        public  Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return _webApiCaller.SendAsync<T>(CrateRequest(HttpMethod.Put, productDto));
        }

        private static ApiRequest CrateRequest(HttpMethod method, string uri = "/api/products")
        {
            return CrateRequest<object>( method, null, uri);
        }
        private static ApiRequest CrateRequest<T>(HttpMethod method, T? data, string uri = "/api/products")
        {
            return new ApiRequest()
            {
                ApiType = method,
                Data = data,
                Url = SD.ProuctAPIBase + uri,
                AccessToken = ""
            };
        }
    }
}
