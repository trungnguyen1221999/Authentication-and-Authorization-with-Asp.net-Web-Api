

namespace Auth.Application.Dtos.Requests
{
    public class SignupRequestDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;   
        public RoleName Role { get; set; }
    }
}
