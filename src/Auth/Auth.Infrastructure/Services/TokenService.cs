using Auth.Application.Services;
using Auth.Domain;
using Auth.Domain.Constants.Claims;
using Auth.Domain.Cores;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Auth.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IPermissionService _permissionService;
        public TokenService(IOptions<JwtSettings> jwtSettings, IPermissionService permissionService)
        {
            _jwtSettings = jwtSettings.Value;
            _permissionService = permissionService;
        }
        public async Task<string> GenerateTokenAsync(AppUser user)
        {
            var permissions = await _permissionService.GetPermissionsForUserAsync(user);
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new (JwtRegisteredClaimNames.Email, user.Email!),
                new (UserClaims.FirstName, user.FirstName),
                new (UserClaims.LastName, user.LastName),
                new (UserClaims.PhoneNumber, user.PhoneNumber ?? string.Empty),
                new (UserClaims.Role, user.Role.ToString()),
                new (UserClaims.IsActive, user.isActive.ToString()),
                new(UserClaims.Permission,JsonSerializer.Serialize(permissions))
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            //Build Token


            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiryHours),
                signingCredentials: creds,
                claims : claims
            );

            //Serialize token to header.payload.signature 

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
