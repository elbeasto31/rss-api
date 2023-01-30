using Microsoft.AspNetCore.Mvc;
using RssApi.Filters.Exceptions;
using RssApi.Models.RequestModels;
using RssApi.Services.Abstractions;

namespace RssApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IAuthService AuthService { get; set; }

        public LoginController(IAuthService authService)
        {
            AuthService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var token = await AuthService.Register(registerModel);
            return Ok(token);
        }

        [HttpPost]
        [Route("login")]
        [NotFoundException]
        public async Task<IActionResult> Login([FromBody] LoginModel credentials)
        {
            var token = await AuthService.SignIn(credentials.UserName, credentials.Password);
            return Ok(token);
        }

    }

}