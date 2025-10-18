using Microservices.Web.Models;
using Microservices.Web.Services.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Microservices.Web.Utility.SD;

namespace Microservices.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _ClientFactory;
        private readonly ITokenProvider _tokenProvider;

        public BaseService(IHttpClientFactory ClientFactory, ITokenProvider tokenProvider)
        {
            _ClientFactory = ClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearerToken = true)
        {
            try
            {
                HttpClient client = _ClientFactory.CreateClient("Microservicios");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");

                //Token
                if (withBearerToken)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDto.Url);

                message.Method = requestDto.ApiType switch
                {
                    ApiType.GET => HttpMethod.Get,
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                if (requestDto.Data != null)
                {
                    var jsonData = JsonConvert.SerializeObject(requestDto.Data);
                    Console.WriteLine($"JsonData: " + jsonData);
                    message.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                }

                Console.WriteLine($"Enviando request a {message.RequestUri} con el metodo: {message.Method}");

                HttpResponseMessage responseMessage = await client.SendAsync(message);

                if (!responseMessage.IsSuccessStatusCode)
                {
                    var errorContent = await responseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {responseMessage.StatusCode}, detalle: {errorContent}");
                }

                switch (responseMessage.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.BadRequest:
                        return new() { IsSuccess = false, Message = "Bad Request" };
                    case HttpStatusCode.MethodNotAllowed:
                        return new() { IsSuccess = false, Message = "Method Not Allowed" };
                    default:
                        var apiContent = await responseMessage.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var newResponse = new ResponseDto
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };

                return newResponse;
            }
        }
    }
}
