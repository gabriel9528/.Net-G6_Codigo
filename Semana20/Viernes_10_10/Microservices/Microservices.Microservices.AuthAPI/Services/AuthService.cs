using Microservices.Microservices.AuthAPI.Data;
using Microservices.Microservices.AuthAPI.Models;
using Microservices.Microservices.AuthAPI.Models.Dto;
using Microservices.Microservices.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Microservices.Microservices.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Nombres = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if(result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(x => x.UserName == registrationRequestDto.Email);
                    UserDto userDto = new UserDto()
                    {
                        Id = userToReturn.Id,
                        Email = registrationRequestDto.Email,
                        Name = registrationRequestDto.Name,
                        PhoneNumber = registrationRequestDto.PhoneNumber
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || !isValid)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new UserDto()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Nombres,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token,
            };

            return loginResponseDto;
        }
        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
            if(user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }

            return false;

        }

        

        
    }
}
