﻿using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands
{
    public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimDto>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimDto>
        {

            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _rules;
            private readonly IUserOperationClaimRepository _userClaimRepository;

            public CreateUserOperationClaimCommandHandler(IMapper mapper, UserOperationClaimBusinessRules rules, IUserOperationClaimRepository userClaimRepository)
            {
                _mapper = mapper;
                _rules = rules;
                _userClaimRepository = userClaimRepository;
            }

            public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _rules.FindUserOperationClaimByClaimId(request.OperationClaimId);

                await _rules.CheckIfUserHasAlreadyRole(request.UserId, request.OperationClaimId);
                UserOperationClaim mappedUserClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim createdUserClaim = await _userClaimRepository.AddAsync(mappedUserClaim);
                CreatedUserOperationClaimDto createdUserClaimDto = _mapper.Map<CreatedUserOperationClaimDto>(createdUserClaim);


                return createdUserClaimDto;

            }
        }
    }
}
