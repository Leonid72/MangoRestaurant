using Mango.Services.ShoppingCartAPI.Models.Dto;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public CouponRepository(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _client.BaseAddress = new Uri(_configuration.
                GetSection("ServiceUrls").GetSection("CouponAPI").Value);
        }

        public async Task<CouponDto> GetCoupon(string couponName)
        {
            var response = await _client.GetAsync($"api/coupon/{couponName}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (result.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(result.Result.ToString());
            }
            return new CouponDto();
        }
    }
}
