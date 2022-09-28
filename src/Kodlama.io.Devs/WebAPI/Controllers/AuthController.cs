using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.Users.Commands.LoginCommand;
using Application.Features.Users.Commands.RegisterCommand;
using Application.Features.Users.Dtos;
using Core.Security.Dtos;
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
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand request)
        {
           UserDto result = await Mediator.Send(request);
            return Created("Registered successfully.", result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        {
            AccessToken result = await Mediator.Send(request);
            return Created("Logined successfully.", result);
        }
    }
}
