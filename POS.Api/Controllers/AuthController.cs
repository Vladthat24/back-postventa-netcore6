using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Aplication.Dtos.User.Request;
using POS.Aplication.Interfaces;
using POS.Aplication.Services;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;

        public AuthController(IAuthApplication authApplication)
        {
            _authApplication = authApplication;
        }

        [AllowAnonymous]
        [HttpPost("Generate/Login")]
        public async Task<IActionResult> Login([FromBody] TokenRequestDto requestDto, [FromQuery] string authType)
        {
            var response = await _authApplication.Login(requestDto,authType);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Generate/LoginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] string credenciales, [FromQuery] string authType)
        {
            var response = await _authApplication.LoginWithGoogle(credenciales,authType);
            return Ok(response);
        }
    }
}
