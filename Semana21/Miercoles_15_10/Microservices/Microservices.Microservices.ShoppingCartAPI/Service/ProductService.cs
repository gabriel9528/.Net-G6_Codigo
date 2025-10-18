using Microservices.Microservices.ShoppingCartAPI.Models.Dto;
using Microservices.Microservices.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Microservices.Microservices.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/ProductAPI");
            var apiContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if(result != null & result.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(result.Result));
            }
            
            return new List<ProductDto>();
        }
    }
}
