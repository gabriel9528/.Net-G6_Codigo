using AutoMapper;
using Microservices.Microservices.CouponAPI.Models;
using Microservices.Microservices.CouponAPI.Models.Dto;

namespace Microservices.Microservices.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
