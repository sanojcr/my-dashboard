using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDashboard.Model.Dtos;
using MyDashboard.Model.Entities.Entities;
using MyDashboard.Repository.Interface;

namespace MyDashboard.Repository
{
    public class AuthRepositoy : IAuthRepositoy
    {
        private readonly MyDashboardlDbContext _dashboardlDbContext;
        public AuthRepositoy(MyDashboardlDbContext dashboardlDbContext)
        {
            _dashboardlDbContext = dashboardlDbContext;
        }

        public async Task<User?> ValidateUserAsync(User user)
        {
            var query = _dashboardlDbContext
                .Users
                .Where(x =>
                x.Username == user.Username
                && x.PasswordHash == user.PasswordHash);
            return await query
                .FirstOrDefaultAsync();
        }

        public async Task<RefreshToken> SaveRefreshTokenAsync(RefreshToken refresh)
        {
            var refreshExist = await _dashboardlDbContext
                .RefreshTokens
                .Where(x =>
                x.UserId == refresh.UserId)
                .FirstOrDefaultAsync();

            if (refreshExist == null)
            {
                _dashboardlDbContext
               .RefreshTokens
               .Add(refresh);
            }
            else
            {
                _dashboardlDbContext
                .RefreshTokens
                .Update(refresh);
            }

            await _dashboardlDbContext
                .SaveChangesAsync();
            return refresh;
        }

        public async Task<User?> GetUserOfRefreshToken(string refreshToken)
        {
            var query = _dashboardlDbContext.RefreshTokens
                .GroupJoin(_dashboardlDbContext.Users,
                 r => r.UserId,
                 u => u.Id,
                 (r, uGrp) => new { r, uGrp }
                )
                .SelectMany(x => x.uGrp.DefaultIfEmpty(),
                (x, u) =>
                new
                {
                    r = x.r,
                    u
                }).Where(x =>
                x.r.Token == refreshToken
                && DateTime.UtcNow < x.r.ExpiresAt
                && DateTime.UtcNow < x.r.RevokedAt)
                .Select(x => x.u);

            return await query
                .FirstOrDefaultAsync();
        }

        public async Task<bool> RegisterAsync(User user)
        {
            var isExist = await _dashboardlDbContext
                .Users
                .Where(x =>
                x.Username == user.Username)
                .FirstOrDefaultAsync();

            if (isExist == null)
            {
                _dashboardlDbContext
                .Users
                .Add(user);

                return await _dashboardlDbContext
                    .SaveChangesAsync() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
