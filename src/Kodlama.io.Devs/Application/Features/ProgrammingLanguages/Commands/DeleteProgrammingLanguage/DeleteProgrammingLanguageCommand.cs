using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>
    {
        public int Id { get; set; }
     

        public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;
            public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper,
                ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _mapper = mapper;
                _programmingLanguageRepository = programmingLanguageRepository;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage? language = await _programmingLanguageRepository.GetAsync(b => b.Id == request.Id);
                _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(language);

                ProgrammingLanguage mappedLanguage = _mapper.Map<ProgrammingLanguage>(language);
                ProgrammingLanguage deletedLanguage = await _programmingLanguageRepository.DeleteAsync(mappedLanguage);
                DeletedProgrammingLanguageDto deletedLanaguageDto = _mapper.Map<DeletedProgrammingLanguageDto>(language);
               
                return deletedLanaguageDto;
            }


            


        }
    }
}
