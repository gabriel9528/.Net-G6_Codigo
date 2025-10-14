using Microservices.Microservices.AuthAPI.Models.Dto;

namespace Microservices.Microservices.AuthAPI.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
