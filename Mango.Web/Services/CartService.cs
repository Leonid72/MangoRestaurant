using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class CartService : ICartService
    {
        private readonly IWebApiCaller _webApiCaller;
        public CartService(IWebApiCaller webApiCaller)
        {
            _webApiCaller = webApiCaller;
        }

        public async Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await _webApiCaller.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<T> ApplyCouponAsync<T>(CartDto cartDto, string token = null)
        {
            return await _webApiCaller.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon",
                AccessToken = token
            });
        }

        public async Task<T> GetCartByUserAsync<T>(string userId, string token = null)
        {
            return await _webApiCaller.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Get,
                Url = SD.ShoppingCartAPIBase + $"/api/cart/GetCart/{userId}",
                AccessToken = token
            });
        }

        public async Task<T> RemoveCouponAsync<T>(string userId, string token = null)
        {
            return await _webApiCaller.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = userId,
                Url = SD.ShoppingCartAPIBase + $"/api/cart/RemoveCoupon",
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
        {
            return await _webApiCaller.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = cartId,
                Url = SD.ShoppingCartAPIBase + $"/api/cart/RemoveCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await _webApiCaller.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + $"/api/cart/UpdateCart",
                AccessToken = token
            });
        }
    }
}
