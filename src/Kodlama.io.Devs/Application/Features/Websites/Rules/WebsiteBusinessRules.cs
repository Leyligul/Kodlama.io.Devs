using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Websites.Rules
{
    public class WebsiteBusinessRules
    {
        private readonly IWebsiteRepository _websiteRepository;

        public WebsiteBusinessRules(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        public async Task UrlCanNotBeDuplicated(string url)
        {
            IPaginate<Website> result = await _websiteRepository.GetListAsync(b => b.Url== url);
            if (result.Items.Any()) throw new BusinessException("Url already exists.");
        }

        public void CheckIfWebsiteExists(Website website)
        {

            if (website == null) throw new BusinessException("Website does not exist.");
        }
    }
}
