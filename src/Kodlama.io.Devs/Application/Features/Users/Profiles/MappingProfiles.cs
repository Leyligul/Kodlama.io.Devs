using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.Users.Commands.LoginCommand;
using Application.Features.Users.Commands.RegisterCommand;
using Application.Features.Users.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Register
            CreateMap<User, RegisteredUserDto>().ReverseMap();
            CreateMap<User, RegisterUserCommand>().ReverseMap();


            //Login
         
            CreateMap<User, LoginUserCommand>().ReverseMap();
        }
    }
}
