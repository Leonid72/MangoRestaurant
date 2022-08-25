using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IProductService :IBaseService
    {
        Task<T> GetAllProductsAsync<T>();
        Task<T> GetProductByIdAsync<T>(int id);
        Task<T> CreataProductAsync<T>(ProductDto productDto);
        Task<T> UpdateProductAsync<T>(ProductDto productDto);
        Task<T> DeleteProductAsync<T>(int id);
    }
}
