using Application.Features.OperationClaims.Commands;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Features.Users.Rules;
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
    public class UpdateUserOperationClaimCommand : IRequest<UpdatedUserOperationClaimDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdatedUserOperationClaimDto>
        {


            private readonly UserOperationClaimBusinessRules _rules;
            private readonly IUserOperationClaimRepository _userClaimRepository;
            private readonly UserBusinessRules _userRules;
            private readonly OperationClaimBusinessRules _operationClaimRules;
            private readonly IMapper _mapper;

            public UpdateUserOperationClaimCommandHandler(UserOperationClaimBusinessRules rules, IUserOperationClaimRepository claimRepository, IUserOperationClaimRepository userClaimRepository, UserBusinessRules userRules, OperationClaimBusinessRules operationClaimRules, IMapper mapper)
            {

                _rules = rules;
                _userClaimRepository = userClaimRepository;
                _userRules = userRules;
                _operationClaimRules = operationClaimRules;
                _mapper = mapper;
            }

            public async Task<UpdatedUserOperationClaimDto> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim? userClaim = await _rules.FindUserOperationClaimById(request.Id);

                await _rules.CheckIfUserHasAlreadyRole(userClaim, request.OperationClaimId);

                User user = await _userRules.FindUserById(request.UserId);
              
                userClaim.OperationClaimId = request.OperationClaimId;
                userClaim.UserId = request.UserId;
  
                UserOperationClaim updatedUserClaim = await _userClaimRepository.UpdateAsync(userClaim);
                UpdatedUserOperationClaimDto updatedUserClaimDto = _mapper.Map<UpdatedUserOperationClaimDto>(updatedUserClaim);


                return updatedUserClaimDto;

            }
        }
    }
}
