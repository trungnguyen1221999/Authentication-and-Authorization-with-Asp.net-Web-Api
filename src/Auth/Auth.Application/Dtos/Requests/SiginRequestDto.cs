using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Dtos.Requests
{
    public class SigninRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
