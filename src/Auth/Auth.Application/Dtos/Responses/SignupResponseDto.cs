using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Dtos.Responses
{
    public class SignupResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public bool isSuccess { get; set; }
    }
}
