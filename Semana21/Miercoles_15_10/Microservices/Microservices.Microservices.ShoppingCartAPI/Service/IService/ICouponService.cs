using Microservices.Microservices.ShoppingCartAPI.Models.Dto;

namespace Microservices.Microservices.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
