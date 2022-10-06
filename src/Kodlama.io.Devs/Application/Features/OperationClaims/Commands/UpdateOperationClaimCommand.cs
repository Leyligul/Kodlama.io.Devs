using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Features.Websites.Commands.UpdateWebsiteCommands;
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
    public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
        {

            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _rules;
            private readonly IOperationClaimRepository _claimRepository;

            public UpdateOperationClaimCommandHandler(IMapper mapper, OperationClaimBusinessRules rules, IOperationClaimRepository claimRepository)
            {
                _mapper = mapper;
                _rules = rules;
                _claimRepository = claimRepository;
            }

            public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim? claim = await _rules.FindOperationClaimById(request.Id);

                await _rules.FindOperationClaimByName(request.Name);

                _mapper.Map<UpdateOperationClaimCommand, OperationClaim>(request, claim);
                OperationClaim updatedClaim = await _claimRepository.UpdateAsync(claim);
                UpdatedOperationClaimDto updatedClaimDto = _mapper.Map<UpdatedOperationClaimDto>(updatedClaim);


                return updatedClaimDto;

            }
        }
    }
}
