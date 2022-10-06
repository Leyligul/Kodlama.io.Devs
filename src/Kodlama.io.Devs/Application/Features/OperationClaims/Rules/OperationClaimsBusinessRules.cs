using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules
    {

        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task FindOperationClaimByName(string name)
        {
           IPaginate<OperationClaim> result = await _operationClaimRepository.GetListAsync(p => p.Name == name);
          
          if (result.Items.Any()) throw new BusinessException("Operation Claim already exists.");
        }

        public async Task<OperationClaim> FindOperationClaimById(int Id)
        {
            OperationClaim result = await _operationClaimRepository.GetAsync(p => p.Id == Id);

            if (result == null) throw new BusinessException("Operation Claim does not exists with this Id.");

            return result;
        }

    }
}
