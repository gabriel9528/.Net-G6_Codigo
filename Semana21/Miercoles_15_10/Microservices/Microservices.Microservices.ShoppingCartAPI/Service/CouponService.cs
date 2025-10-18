using Microservices.Microservices.ShoppingCartAPI.Models.Dto;
using Microservices.Microservices.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Microservices.Microservices.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/CouponAPI/getByCode/{couponCode}");
            var apiContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if(result != null && result.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(result.Result));
            }

            return new CouponDto();
        }
    }
}
