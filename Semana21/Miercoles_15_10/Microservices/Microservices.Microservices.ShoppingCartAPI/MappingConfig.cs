using AutoMapper;
using Microservices.Microservices.ShoppingCartAPI.Models;
using Microservices.Microservices.ShoppingCartAPI.Models.Dto;

namespace Microservices.Microservices.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
