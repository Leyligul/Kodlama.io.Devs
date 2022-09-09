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

namespace Application.Features.ProgrammingLanguageTechnologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommand : IRequest<DeletedTechnologyDto>
    {
        public int Id { get; set; }


        public class DeleteDeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageTechnologysRules _programmingLanguageTechnologyBusinessRules;

            public DeleteDeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, ProgrammingLanguageTechnologysRules programmingLanguageTechnologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _programmingLanguageTechnologyBusinessRules = programmingLanguageTechnologyBusinessRules;
            }

            public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguageTechnology? existTechnology = await _technologyRepository.GetAsync(x => x.Id == request.Id);

                _programmingLanguageTechnologyBusinessRules.ProgrammingLanguageTechnologyShouldExistWhenRequested(existTechnology);

                ProgrammingLanguageTechnology deleteTechnology = await _technologyRepository.DeleteAsync(existTechnology);
                DeletedTechnologyDto deletedTechnologyDto = _mapper.Map<DeletedTechnologyDto>(deleteTechnology);

                return deletedTechnologyDto;
            }
        }
    }
}
