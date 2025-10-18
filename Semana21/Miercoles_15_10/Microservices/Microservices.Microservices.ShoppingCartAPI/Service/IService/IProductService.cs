using Microservices.Microservices.ShoppingCartAPI.Models.Dto;

namespace Microservices.Microservices.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
