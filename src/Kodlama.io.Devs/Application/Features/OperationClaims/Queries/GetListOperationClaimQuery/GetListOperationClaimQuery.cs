using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Rules;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaimQuery
{
    public class GetListOperationClaimQuery : IRequest<OperationClaimListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, OperationClaimListModel>
        {
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _rules;
            private readonly IOperationClaimRepository _claimRepository;

            public GetListOperationClaimQueryHandler(IMapper mapper, OperationClaimBusinessRules rules, IOperationClaimRepository claimRepository)
            {
                _mapper = mapper;
                _rules = rules;
                _claimRepository = claimRepository;
            }

            public async Task<OperationClaimListModel> Handle(GetListOperationClaimQuery request, CancellationToken cancellation)
            {

                IPaginate<OperationClaim> claims = await _claimRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                OperationClaimListModel mapperOperationClaimListModel = _mapper.Map<OperationClaimListModel>(claims);
                return mapperOperationClaimListModel;
            }
        }
    }
}
