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
        private readonly IOperationClaimRepository _operationClaimRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userClaimRepository, IOperationClaimRepository operationClaimRepository)
        {
            _userClaimRepository = userClaimRepository;
            _operationClaimRepository = operationClaimRepository;
        }

   

        public async Task<UserOperationClaim> FindUserOperationClaimById(int Id)
        {
            UserOperationClaim result = await _userClaimRepository.GetAsync(p => p.Id == Id);

            if (result == null) throw new BusinessException("User Operation Claim does not exists.");

            return result;
        }

        public async Task<UserOperationClaim> CheckUserOperationClaimAlreadyCreated(int Id)
        {
            UserOperationClaim result = await _userClaimRepository.GetAsync(p => p.Id == Id);

            if (result != null) throw new BusinessException("User Operation Claim already exists.");

            return result;
        }

        public async Task CheckIfUserHasAlreadyRole(UserOperationClaim claim, int requestClaimId)
        {
        
            if (claim.OperationClaimId == requestClaimId) throw new BusinessException("User already has this role.");
   
        }
    }
}
