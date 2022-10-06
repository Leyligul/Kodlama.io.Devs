using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Rules;
using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries.GetListUserOperationClaimQuery
{
    public class GetListUserOperationClaimQuery : IRequest<UserOperationClaimListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery, UserOperationClaimListModel>
        {
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _rules;
            private readonly IUserOperationClaimRepository _userClaimRepository;

            public GetListUserOperationClaimQueryHandler(IMapper mapper, UserOperationClaimBusinessRules rules, IOperationClaimRepository claimRepository, IUserOperationClaimRepository userClaimRepository)
            {
                _mapper = mapper;
                _rules = rules;
                _userClaimRepository = userClaimRepository;
            }

            public async Task<UserOperationClaimListModel> Handle(GetListUserOperationClaimQuery request, CancellationToken cancellation)
            {

                IPaginate<UserOperationClaim> claims = await _userClaimRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                UserOperationClaimListModel mapperUserOperationClaimListModel = _mapper.Map<UserOperationClaimListModel>(claims);
                return mapperUserOperationClaimListModel;
            }
        }
    }
}
