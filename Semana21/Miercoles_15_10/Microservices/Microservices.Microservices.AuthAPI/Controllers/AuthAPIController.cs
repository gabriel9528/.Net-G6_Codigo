using Microservices.Microservices.AuthAPI.Models.Dto;
using Microservices.Microservices.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Microservices.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                _responseDto.Result = false;
                return BadRequest(_responseDto);
            }

            _responseDto.Message = "Usuario registrado exitosamente";
            _responseDto.Result = true;
            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.Login(loginRequestDto);
            if (loginResponse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username o pasword incorrecto";
                _responseDto.Result = false;
                return BadRequest(_responseDto);
            }

            _responseDto.Result = loginResponse;
            _responseDto.Message = "Usuario logueaDo exitosamente";
            return Ok(_responseDto);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequestDto assignRoleRequestDto)
        {
            var assignRoleSuccessfully = await _authService.AssignRole(assignRoleRequestDto.Email,
                assignRoleRequestDto.Role);

            if (!assignRoleSuccessfully)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "No se puso asignar el rol.";
                _responseDto.Result = false;
                return BadRequest(_responseDto);
            }

            _responseDto.Result = true;
            _responseDto.Message = "Rol asignado correctamente";
            return Ok(_responseDto);
        }
    }
}
