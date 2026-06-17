using Auth.Application.Dtos.Requests;
using Auth.Application.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Services
{
    public interface ISignupService
    {
        Task<SignupResponseDto> Signup(SignupRequestDto request);
    }
}
