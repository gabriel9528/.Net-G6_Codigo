using Microservices.Web.Models;
using Microservices.Web.Models.AuthDtos;

namespace Microservices.Web.Services.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(AssignRoleRequestDto assignRoleRequestDto);
    }
}
