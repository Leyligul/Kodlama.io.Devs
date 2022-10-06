using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
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

        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
        {

            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _rules;
            private readonly IUserOperationClaimRepository _userClaimRepository;

         

            public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _rules.FindUserOperationClaimByClaimId(request.OperationClaimId);

                OperationClaim mappedClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim createdClaim = await _claimRepository.AddAsync(mappedClaim);
                CreatedOperationClaimDto createdClaimDto = _mapper.Map<CreatedOperationClaimDto>(createdClaim);


                return createdClaimDto;

            }
        }
    }
}
