using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class WebsiteRepository : EfRepositoryBase<Website, BaseDbContext>, IWebsiteRepository
    {
        public WebsiteRepository(BaseDbContext context) : base(context)
        {
        }

        public User GetUser(int userId)
        {
            var result = from Website in Context.Websites
                         join User in Context.Users

                             on Website.UserId equals User.Id
                         where User.Id == userId

                         select new User 
                         { 
                             Id = User.Id,
                             FirstName = User.FirstName,
                             LastName = User.LastName,
                             AuthenticatorType = User.AuthenticatorType,
                             Email = User.Email,    
                             PasswordHash = User.PasswordHash,
                             PasswordSalt = User.PasswordSalt,
                         };

            return result.FirstOrDefault();
        }
    }
}
