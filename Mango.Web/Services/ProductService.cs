using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json.Linq;
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

        public  Task<T> CreataProductAsync<T>(ProductDto productDto,string token)
        {
            return _webApiCaller.SendAsync<T>(CrateRequest(HttpMethod.Post,productDto, token));
        }

        //public  Task<T> DeleteProductAsync<T>(int id)
        //{
        //    return _webApiCaller.SendAsync<T>(CrateRequest(HttpMethod.Delete, $"/api/products/{id}"));
        //}
        public async Task<T> DeleteProductAsync<T>(int id, string token)
        {
            return await _webApiCaller.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Delete,
                Url = SD.ProuctAPIBase + "/api/products/" + id,
                AccessToken = token
            });
        }
        public  Task<T> GetAllProductsAsync<T>(string token)
        {
            return _webApiCaller.SendAsync<T>(CrateRequest(HttpMethod.Get, token));
        }

        public  Task<T> GetProductByIdAsync<T>(int id, string token)
        {
            return _webApiCaller.SendAsync<T>(CrateRequest(HttpMethod.Get,token, $"/api/products/{id}"));
        }

        public  Task<T> UpdateProductAsync<T>(ProductDto productDto, string token)
        {
            return _webApiCaller.SendAsync<T>(CrateRequest(HttpMethod.Put, productDto, token));
        }

        private static ApiRequest CrateRequest(HttpMethod method, string token, string uri = "/api/products")
        {
            return CrateRequest<object>( method, null, token, uri);
        }
        private static ApiRequest CrateRequest<T>(HttpMethod method, T? data, string token, string uri = "/api/products")
        {
            return new ApiRequest()
            {
                ApiType = method,
                Data = data,
                Url = SD.ProuctAPIBase + uri,
                AccessToken = token
            };
        }
    }
}
