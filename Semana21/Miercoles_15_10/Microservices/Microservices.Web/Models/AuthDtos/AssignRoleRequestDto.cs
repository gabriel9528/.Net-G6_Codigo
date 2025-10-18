namespace Microservices.Web.Models.AuthDtos
{
    public class AssignRoleRequestDto
    {
        public string Email { get; set; }
        public string? Role { get; set; }
    }
}
