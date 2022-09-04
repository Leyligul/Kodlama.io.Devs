using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
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

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;
            public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper,
                ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _mapper = mapper;
                _programmingLanguageRepository = programmingLanguageRepository;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }


            public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage? existLanguage = await _programmingLanguageRepository.GetAsync(l => l.Id == request.Id);
                _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(existLanguage);

                await _programmingLanguageBusinessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

                _mapper.Map<UpdateProgrammingLanguageCommand, ProgrammingLanguage>(request, existLanguage);
                // ProgrammingLanguage mappedLanguage = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage updatedLanguage = await _programmingLanguageRepository.UpdateAsync(existLanguage);
                UpdatedProgrammingLanguageDto updatedLanaguageDto = _mapper.Map<UpdatedProgrammingLanguageDto>(updatedLanguage);
             

                return updatedLanaguageDto;
            }
        }
    }
}
