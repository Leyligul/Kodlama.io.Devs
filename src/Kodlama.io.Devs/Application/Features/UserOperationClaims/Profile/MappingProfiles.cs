using Application.Features.OperationClaims.Commands;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.UserOperationClaims.Commands;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Create 
            CreateMap<UserOperationClaim, CreatedUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();

            //Update
            CreateMap<UserOperationClaim, UpdatedUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();

            //Delete
            CreateMap<UserOperationClaim, DeletedUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, DeleteUserOperationClaimCommand>().ReverseMap();

            //Get 
            CreateMap<IPaginate<UserOperationClaim>, UserOperationClaimListModel>().ReverseMap();
            CreateMap<UserOperationClaim, UserOperationClaimListDto>().ReverseMap();

        }
    }
}
