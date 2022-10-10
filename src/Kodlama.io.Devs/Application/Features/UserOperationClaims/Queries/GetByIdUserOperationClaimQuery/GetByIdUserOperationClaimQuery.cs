using Application.Features.OperationClaims.Rules;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguageQuery;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Features.Users.Rules;
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

namespace Application.Features.UserOperationClaims.Queries.GetByIdUserOperationClaimQuery
{
    public class GetByIdUserOperationClaimQuery : IRequest<UserOperationClaimGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdUserOperationClaimQuerHandler : IRequestHandler<GetByIdUserOperationClaimQuery, UserOperationClaimGetByIdDto>
        {
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _rules;
            private readonly UserBusinessRules _userRules;
            private readonly OperationClaimBusinessRules _operationRules;

            public GetByIdUserOperationClaimQuerHandler(IMapper mapper, IUserOperationClaimRepository repository, UserOperationClaimBusinessRules rules, OperationClaimBusinessRules operationRules, UserBusinessRules userRules)
            {
                _mapper = mapper;
                _rules = rules;
                _operationRules = operationRules;
                _userRules = userRules;
            }

            public async Task<UserOperationClaimGetByIdDto> Handle(GetByIdUserOperationClaimQuery request, CancellationToken cancellation)
            {

                UserOperationClaim userClaim = await _rules.FindUserOperationClaimById(request.Id);

                //User user = await _userRules.FindUserById(userClaim.UserId);
                //OperationClaim claim = await _operationRules.FindOperationClaimById(request.Id);

                //userClaim.OperationClaim = claim;
                //userClaim.User = user;
 
                UserOperationClaimGetByIdDto userClaimGetByIdDto = _mapper.Map<UserOperationClaimGetByIdDto>(userClaim);

                return userClaimGetByIdDto;

            }
        }
    }
}
