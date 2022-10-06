using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.Users.Commands.LoginCommand;
using Application.Features.Users.Commands.RegisterCommand;
using Application.Features.Users.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto UserForRegisterDto)
        {
            RegisterUserCommand registerCommand = new()
            {
                UserForRegisterDto = UserForRegisterDto,
                IpAddress = GetIpAddress()
            };

            RegisteredUserDto result = await Mediator.Send(registerCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto UserForLoginDto)
        {
            LoginUserCommand LoginCommand = new()
            {
                UserForLoginDto = UserForLoginDto,
                IpAddress = GetIpAddress()
            };

            LoginedUserDto result = await Mediator.Send(LoginCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
