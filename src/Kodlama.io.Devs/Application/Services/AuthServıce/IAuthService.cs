using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthServıce
{
    public interface IAuthService
    {
        public Task<AccessToken> CreateAccessToken(User user);

        public Task<RefreshToken> AddRefreshToken(RefreshToken token);
        public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);

    }
}
