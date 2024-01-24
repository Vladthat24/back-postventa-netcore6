using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Aplication.Dtos.User.Request;
using POS.Aplication.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequestDto requestDto)
        {
            var response= await _userApplication.RegisterUser(requestDto);
            return Ok(response);
        }

    }
}
