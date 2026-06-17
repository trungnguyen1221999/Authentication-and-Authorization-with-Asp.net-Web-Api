using Auth.Application.Dtos.Requests;
using Auth.Application.Dtos.Responses;
using Auth.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISignupService _signupService;
        private readonly ISigninService _signinService;

        public AuthController(ISignupService signupService, ISigninService signinService)
        {
            _signupService = signupService;
            _signinService = signinService;
        }

        [HttpPost]
        public async Task<ActionResult<SignupResponseDto>> SignupAsync([FromBody] SignupRequestDto request)
        {
            var result = await _signupService.Signup(request);
            return result.isSuccess ? Ok(result)
                : BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult<SiginResponseDto>> SigninAsync([FromBody] SigninRequestDto request)
        {
            var result = await _signinService.Signin(request);
            return result.isSuccess ? Ok(result)
                : BadRequest(result);
        }
    }
}
