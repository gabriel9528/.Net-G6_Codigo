using Microservices.Web.Models;
using Microservices.Web.Services.IService;
using Microservices.Web.Utility;

namespace Microservices.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        //Post
        public async Task<ResponseDto?> CreateCouponAsync(CouponRequestDto couponRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = couponRequestDto,
                Url = SD.CouponAPIBase + "/api/CouponAPI"
            });
        }

        //Update
        public async Task<ResponseDto?> UpdateCouponAsync(CouponRequestDto couponRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponRequestDto,
                Url = SD.CouponAPIBase + "/api/CouponAPI"
            });
        }

        //Delete
        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/CouponAPI/" + id
            });
        }


        //GetAll
        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/CouponAPI"
            });
        }

        //GetCouponByCode
        public async Task<ResponseDto?> GetCouponByCodeAsync(string code)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/CouponAPI/" + $"getByCode/{code}"
            });
        }

        //GetCouponById
        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/CouponAPI/" + id
            });
        }
    }
}
