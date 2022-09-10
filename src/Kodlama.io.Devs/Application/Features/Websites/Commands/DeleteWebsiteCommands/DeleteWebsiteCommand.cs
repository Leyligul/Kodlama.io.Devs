using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.Websites.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Websites.Commands.DeleteWebsiteCommands
{
    public class DeleteWebsiteCommand : IRequest<DeletedWebsiteDto>
    {
        public int Id { get; set; }

        public class DeleteWebsiteCommandHandler : IRequestHandler<DeleteWebsiteCommand, DeletedWebsiteDto>
        {
            private readonly IWebsiteRepository _websiteRepository;
            private readonly IMapper _mapper;

            public DeleteWebsiteCommandHandler(IWebsiteRepository websiteRepository, IMapper mapper)
            {
                _websiteRepository = websiteRepository;
                _mapper = mapper;
            }

            public async Task<DeletedWebsiteDto> Handle(DeleteWebsiteCommand request, CancellationToken cancellationToken)
            {
                Website? website = await _websiteRepository.GetAsync(l => l.Id == request.Id);

                Website deletedWebsite = await _websiteRepository.DeleteAsync(website);
                DeletedWebsiteDto deletedWebsiteDto = _mapper.Map<DeletedWebsiteDto>(deletedWebsite);

                return deletedWebsiteDto;
            }
        }
    }
}
