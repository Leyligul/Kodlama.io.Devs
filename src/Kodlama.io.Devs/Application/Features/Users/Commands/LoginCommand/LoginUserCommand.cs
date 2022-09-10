using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
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
    public class LoginUserCommand : IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _rules;

            public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules rules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _rules = rules;
            }

            public async Task<AccessToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {

                //CheckUserByMail
                var user = await _userRepository.GetAsync(u => u.Email == request.Email);
                _rules.CheckIfUserExists(user);


                //VerifyPassword
                await _rules.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt);


                // User user = _mapper.Map<User>(request);
                var userClaims = _userRepository.GetClaims(user);
                var accessToken = _tokenHelper.CreateToken(user, userClaims);
                return accessToken;

            }
        }
    }
}
