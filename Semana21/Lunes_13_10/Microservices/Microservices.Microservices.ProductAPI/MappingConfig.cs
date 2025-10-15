using AutoMapper;
using Microservices.Microservices.ProductAPI.Models;
using Microservices.Microservices.ProductAPI.Models.Dto;

namespace Microservices.Microservices.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
