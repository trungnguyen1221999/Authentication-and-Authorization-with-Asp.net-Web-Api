using Auth.Application.Dtos.Requests;
using Auth.Application.Dtos.Responses;
using Auth.Application.Services;
using Auth.Domain.Cores;
using Microsoft.AspNetCore.Identity;


namespace Auth.Infrastructure.Services
{
    public class SignupService : ISignupService
    {
        private readonly UserManager<AppUser> _userManager;
        public SignupService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<SignupResponseDto> Signup(SignupRequestDto request)
        {
            var result = new SignupResponseDto();
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if(existingUser != null)
            {
                result.isSuccess = false;
                result.Message = "Email is already in use";
                return result;
            }
            var newUser = new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                Role = request.Role,
                isActive = true
            };
            var createResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!createResult.Succeeded)
            {
                result.isSuccess = false;
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                result.Message = "Failed to create user: " + errors;
                return result;
            }
            result.isSuccess = true;
            result.Message = "User created successfully";
            return result;
        }
    }
}
