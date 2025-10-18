using Microservices.Web.Models;
using Microservices.Web.Models.ProductDtos;

namespace Microservices.Web.Services.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> CreateProductAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProductAsync(int id);
    }
}
