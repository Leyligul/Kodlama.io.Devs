using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.ProgrammingLanguageTechnologies.Dtos;
using Application.Features.ProgrammingLanguageTechnologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageTechnologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }

        public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageTechnologysRules _programmingLanguageTechnologyBusinessRules;

            public UpdateProgrammingLanguageCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, ProgrammingLanguageTechnologysRules programmingLanguageTechnologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _programmingLanguageTechnologyBusinessRules = programmingLanguageTechnologyBusinessRules;
            }

            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                var existsTechnology = await _technologyRepository.GetAsync(d => d.Id == request.Id);
                _programmingLanguageTechnologyBusinessRules.ProgrammingLanguageTechnologyShouldExistWhenRequested(existsTechnology);

                await _programmingLanguageTechnologyBusinessRules.CanNotBeDuplicatedWhenInserted(request.Name);

                _mapper.Map<UpdateTechnologyCommand, ProgrammingLanguageTechnology>(request, existsTechnology);
             
                ProgrammingLanguageTechnology updatedTechnology = await _technologyRepository.UpdateAsync(existsTechnology);
                UpdatedTechnologyDto updatedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);

                return updatedTechnologyDto;
            }
        }
    }
}
