using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.Users.Rules;
using Application.Features.Websites.Dtos;
using Application.Features.Websites.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Websites.Commands.UpdateWebsiteCommands
{
    public class UpdateWebsiteCommand : IRequest<UpdatedWebsiteDto>
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int UserId { get; set; }
        public class UpdateWebsiteCommandHandler : IRequestHandler<UpdateWebsiteCommand, UpdatedWebsiteDto>
        {
            private readonly IWebsiteRepository _websiteRepository;
            private readonly IMapper _mapper;
            private readonly WebsiteBusinessRules _rules;
            private readonly UserBusinessRules _userRules;

            public UpdateWebsiteCommandHandler(IWebsiteRepository websiteRepository, IMapper mapper, WebsiteBusinessRules rules, UserBusinessRules userRules)
            {
                _websiteRepository = websiteRepository;
                _mapper = mapper;
                _rules = rules;
                _userRules = userRules;
            }

            public async Task<UpdatedWebsiteDto> Handle(UpdateWebsiteCommand request, CancellationToken cancellationToken)
            {
                //CheckUser
                User user = _websiteRepository.GetUser(request.UserId);
                _userRules.CheckIfUserExists(user);

                Website? existWebsite = await _websiteRepository.GetAsync(l => l.Id == request.Id);
                _rules.CheckIfWebsiteExists(existWebsite);

                //Dublicate Url
                await _rules.UrlCanNotBeDuplicated(request.Url);
              
                _mapper.Map<UpdateWebsiteCommand, Website>(request, existWebsite);

                Website updatedWebsite = await _websiteRepository.UpdateAsync(existWebsite);
                UpdatedWebsiteDto updatedWebsiteDto = _mapper.Map<UpdatedWebsiteDto>(updatedWebsite);


                return updatedWebsiteDto;
            }
        }
    }
}
