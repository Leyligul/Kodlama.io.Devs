using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
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
    public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimDto> , ISecuredRequest
    {
        public string Name { get; set; }

        public string[] Roles => new[]  { "Admin"};

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
        {
         
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _rules;
            private readonly IOperationClaimRepository _claimRepository;

            public CreateOperationClaimCommandHandler(IMapper mapper, OperationClaimBusinessRules rules, IOperationClaimRepository claimRepository)
            {
                _mapper = mapper;
                _rules = rules;
                _claimRepository = claimRepository;
            }

            public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _rules.FindOperationClaimByName(request.Name);

                OperationClaim mappedClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim createdClaim = await _claimRepository.AddAsync(mappedClaim);
                CreatedOperationClaimDto createdClaimDto = _mapper.Map<CreatedOperationClaimDto>(createdClaim);


                return createdClaimDto;

            }
        }
    }
}
