using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Websites.Commands.CreateWebsiteCommands
{
    public class CreateWebsiteCommand : IRequest<CreatedWebsiteDto>
    {
        public string Url { get; set; }

        public int UserId { get; set; }

        public string Password { get; set; }

        public class CreateWebsiteCommandHandler : IRequestHandler<CreateWebsiteCommand, CreatedWebsiteDto>
        {
            private readonly IWebsiteRepository _websiteRepository;
            private readonly IMapper _mapper;
            private readonly WebsiteBusinessRules _rules;
            private readonly UserBusinessRules _userRules;

            public CreateWebsiteCommandHandler(IWebsiteRepository websiteRepository, IMapper mapper, WebsiteBusinessRules rules, UserBusinessRules userRules)
            {
                _websiteRepository = websiteRepository;
                _mapper = mapper;
                _rules = rules;
                _userRules = userRules;
            }

            public async Task<CreatedWebsiteDto> Handle(CreateWebsiteCommand request, CancellationToken cancellationToken)
            {
                //CheckUser
           

                //Dublicate Url
                await _rules.UrlCanNotBeDuplicated(request.Url);

                //CheckUserAuthentication
                //await _userRules.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt);
                
               
           
                Website mappedWebsite = _mapper.Map<Website>(request);
                Website createdWebsite = await _websiteRepository.AddAsync(mappedWebsite);
                CreatedWebsiteDto createdWebsiteDto = _mapper.Map<CreatedWebsiteDto>(createdWebsite);


                return createdWebsiteDto;
            }
        }
    }
}
