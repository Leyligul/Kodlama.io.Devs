using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Features.ProgrammingLanguages.Rules
{

    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguage;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguage)
        {
            _programmingLanguage = programmingLanguage;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguage.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Name already exists.");
        }

        public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage programmingLanguage)
       {
            
           if (programmingLanguage == null) throw new BusinessException("Requested programmingLanguage does not exist.");
       }

   
    }
}
