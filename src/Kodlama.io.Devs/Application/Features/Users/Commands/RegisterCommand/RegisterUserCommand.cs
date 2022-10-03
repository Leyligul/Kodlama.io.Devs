using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.RegisterCommand
{

    public class RegisterUserCommand : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _rules;
            private readonly ITokenHelper _tokenHelper;

            public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules rules, ITokenHelper tokenHelper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _rules = rules;
                _tokenHelper = tokenHelper;
            }

            public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                await _rules.EmailCanNotBeDuplicated(request.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                User user = _mapper.Map<User>(request);
                user.Status = true;
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;
                user.AuthenticatorType = AuthenticatorType.Email;

                await _userRepository.AddAsync(user);

                var userClaims = _userRepository.GetClaims(user);
                _tokenHelper.CreateToken(user, userClaims);

                UserDto userDto = _mapper.Map<UserDto>(user);

                return userDto;

            }
        }
    }
}
