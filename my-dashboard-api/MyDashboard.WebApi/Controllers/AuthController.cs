using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDashboard.Model.Dtos;
using MyDashboard.Service;
using MyDashboard.Service.Interface;

namespace MyDashboard.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            var result = await _authService.LoginAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto token)
        {
            var result = await _authService.RefreshTokenAsync(token);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto user)
        {
            var result = await _authService.RegisterAsync(user);
            return Ok(result);
        }

    }
}
