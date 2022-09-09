using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.ProgrammingLanguageTechnologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageTechnologies.Query.GetListTechnology
{
    public class GetListTechnologyQuery:IRequest<TechnologyListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, TechnologyListModel>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public GetListTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<TechnologyListModel> Handle(GetListTechnologyQuery request, CancellationToken cancellation)
            {

                IPaginate<ProgrammingLanguageTechnology> programmingLanguagesTechnology = await _technologyRepository.GetListAsync(
                    include:
                    m => m.Include (c => c.Language),
                    index: request.PageRequest.Page, 
                    size: request.PageRequest.PageSize);

                TechnologyListModel model = _mapper.Map<TechnologyListModel>(programmingLanguagesTechnology);



                return model;
            }
        }
    }
}
