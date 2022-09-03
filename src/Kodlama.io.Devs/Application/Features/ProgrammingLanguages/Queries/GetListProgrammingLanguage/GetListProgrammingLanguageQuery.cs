using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage
{
    public class GetListProgrammingLanguageQuery : IRequest<ProgrammingLanguageListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListProgrammingLanguageQueryHandler : IRequestHandler<GetListProgrammingLanguageQuery, ProgrammingLanguageListModel>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public GetListProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageQuery request, CancellationToken cancellation)
            {
             
                IPaginate<ProgrammingLanguage> programmingLanguages = await _programmingLanguageRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                ProgrammingLanguageListModel mapperBrandListModel = _mapper.Map<ProgrammingLanguageListModel>(programmingLanguages);
               
          

                return mapperBrandListModel;
            }
        }
    }
}
