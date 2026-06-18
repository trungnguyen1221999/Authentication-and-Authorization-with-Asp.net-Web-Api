using Auth.Domain.Cores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync (AppUser user);
    }
}
