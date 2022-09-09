using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguageTechnologies.Commands.CreateTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Commands.DeleteTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Commands.UpdateTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Dtos;
using Application.Features.ProgrammingLanguageTechnologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageTechnologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Create 
            CreateMap<ProgrammingLanguageTechnology, CreateTechnologyCommand>().ReverseMap();
            CreateMap<ProgrammingLanguageTechnology, CreatedTechnologyDto>().ReverseMap();
         

            //Get 
            CreateMap<ProgrammingLanguageTechnology, TechnologyListDto>().ForMember(c => c.ProgrammingLanguageName,
                opt => opt.MapFrom(c => c.Language.Name)).ReverseMap();
            CreateMap<IPaginate<ProgrammingLanguageTechnology>, TechnologyListModel>().ReverseMap();



            //Update
            CreateMap<UpdateTechnologyCommand, ProgrammingLanguageTechnology>().ReverseMap();
            CreateMap<UpdatedTechnologyDto, ProgrammingLanguageTechnology>().ReverseMap();


            //Delete
            CreateMap<ProgrammingLanguageTechnology, DeletedTechnologyDto>().ReverseMap();
            CreateMap<ProgrammingLanguageTechnology, DeleteTechnologyCommand>().ReverseMap();

        }
    }
}
