using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IWebApiCaller _webApiCaller;
        public CouponService(IWebApiCaller webApiCaller)
        {
            _webApiCaller = webApiCaller;   
        }
        public async Task<T> GetCoupon<T>(string couponCode, string token = null)
        {
            return await _webApiCaller.SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = HttpMethod.Get,
                Data = couponCode,
                Url = SD.CouponAPIBase + "/api/coupon/" + couponCode,
                AccessToken = token
            });
        }
    }
}
