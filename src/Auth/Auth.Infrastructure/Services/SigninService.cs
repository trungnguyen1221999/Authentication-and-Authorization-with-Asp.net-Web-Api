using Auth.Application.Dtos.Requests;
using Auth.Application.Dtos.Responses;
using Auth.Application.Services;
using Auth.Domain.Cores;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Services
{
    public class SigninService : ISigninService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;

        public SigninService (UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }
        public async Task<SiginResponseDto> Signin(SigninRequestDto request)
        {
            var result = new SiginResponseDto();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                result.isSuccess = false;
                result.ErrorMessage = "User not found";
                return result;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                result.isSuccess = false;
                result.ErrorMessage = "Invalid password";
                return result;
            }
            result.isSuccess = true;
            result.Token = await _tokenService.GenerateTokenAsync(user);
            result.ExpierTime = DateTime.UtcNow.AddDays(1);

            return result;
        }
    }
}
