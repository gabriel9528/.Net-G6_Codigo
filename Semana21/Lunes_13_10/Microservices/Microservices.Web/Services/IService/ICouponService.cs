using Microservices.Web.Models;
using Microservices.Web.Models.CouponDtos;

namespace Microservices.Web.Services.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponByCodeAsync(string code);
        Task<ResponseDto?> CreateCouponAsync(CouponRequestDto couponRequestDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponRequestDto couponRequestDto);
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}
