using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageTechnologies.Rules
{
    public class ProgrammingLanguageTechnologysRules
    {
        private readonly ITechnologyRepository _technology;

        public ProgrammingLanguageTechnologysRules(ITechnologyRepository technology)
        {
            _technology = technology;
        }

        public async Task CanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguageTechnology> result = await _technology.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException("Name already exists.");
        }

        public void ProgrammingLanguageTechnologyShouldExistWhenRequested(ProgrammingLanguageTechnology programmingLanguageTechnology)
        {

            if (programmingLanguageTechnology == null) throw new BusinessException("Requested does not exist.");
        }

      
        
    }
}
