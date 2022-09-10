using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.Websites.Commands.CreateWebsiteCommands;
using Application.Features.Websites.Commands.DeleteWebsiteCommands;
using Application.Features.Websites.Commands.UpdateWebsiteCommands;
using Application.Features.Websites.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Websites.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Create 
            CreateMap<Website, CreatedWebsiteDto>().ReverseMap();
            CreateMap<Website, CreateWebsiteCommand>().ReverseMap();

            //Update
            CreateMap<Website, UpdatedWebsiteDto>().ReverseMap();
            CreateMap<Website, UpdateWebsiteCommand>().ReverseMap();

            //Delete
            CreateMap<Website, DeletedWebsiteDto>().ReverseMap();
            CreateMap<Website, DeleteWebsiteCommand>().ReverseMap();

        }
    }
}
