using Application.Features.OperationClaims.Dtos;
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
    public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimDto>
    {
        public int Id { get; set; }

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimDto>
        {

            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _rules;
            private readonly IUserOperationClaimRepository _claimRepository;

            public DeleteOperationClaimCommandHandler(IMapper mapper, UserOperationClaimBusinessRules rules, IUserOperationClaimRepository claimRepository)
            {
                _mapper = mapper;
                _rules = rules;
                _claimRepository = claimRepository;
            }

            public async Task<DeletedUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim userClaim = await _rules.FindUserOperationClaimById(request.Id);
                UserOperationClaim deletedUserClaim = await _claimRepository.DeleteAsync(userClaim);
                DeletedUserOperationClaimDto deletedUserOperationClaimDto = _mapper.Map<DeletedUserOperationClaimDto>(deletedUserClaim);

                return deletedUserOperationClaimDto;
            }
        }
    }
}
