using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Features.Websites.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands
{
    public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>
    {
        public int Id { get; set; }

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
        {

            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _rules;
            private readonly IOperationClaimRepository _claimRepository;

            public DeleteOperationClaimCommandHandler(IMapper mapper, OperationClaimBusinessRules rules, IOperationClaimRepository claimRepository)
            {
                _mapper = mapper;
                _rules = rules;
                _claimRepository = claimRepository;
            }

            public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim claim = await _rules.FindOperationClaimById(request.Id);

                OperationClaim deletedClaim = await _claimRepository.DeleteAsync(claim);
                DeletedOperationClaimDto deletedOperationClaimDto = _mapper.Map<DeletedOperationClaimDto>(deletedClaim);

                return deletedOperationClaimDto;
            }
        }
    }
}
