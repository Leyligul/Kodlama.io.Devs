using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.AuthServıce;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.LoginCommand
{
    public class LoginUserCommand : IRequest<LoginedUserDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginedUserDto>
        {
            private readonly IAuthService _authService;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _rules;

            public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules rules, IAuthService authService)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _rules = rules;
                _authService = authService;
            }

            public async Task<LoginedUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {

                //CheckUserByMail
                User user = await _rules.FindUser(request.UserForLoginDto.Email);


                //VerifyPassword
                await _rules.VerifyPassword(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                LoginedUserDto loginedUserDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken,

                };
                return loginedUserDto;


            }
        }
    }
}
