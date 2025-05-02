using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDashboard.Model.Entities.Entities;

namespace MyDashboard.Repository.Interface
{
    public interface IAuthRepositoy
    {
        Task<User?> ValidateUserAsync(User user);
        Task<RefreshToken> SaveRefreshTokenAsync(RefreshToken refresh);
        Task<User?> GetUserOfRefreshToken(string refreshToken);
        Task<bool> RegisterAsync(User user);
    }
}
