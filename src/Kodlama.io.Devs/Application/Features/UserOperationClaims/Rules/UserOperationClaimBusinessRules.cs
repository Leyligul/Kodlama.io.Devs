using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
  public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userClaimRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userClaimRepository)
        {
            _userClaimRepository = userClaimRepository;
        }

        public async Task<UserOperationClaim> FindUserOperationClaimByClaimId(int claimId)
        {
            UserOperationClaim result = await _userClaimRepository.GetAsync(p => p.OperationClaimId == claimId);

            if (result == null) throw new BusinessException("Operation Claim does not exists.");

            return result;
        }
    }
}
