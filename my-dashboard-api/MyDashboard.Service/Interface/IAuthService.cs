using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDashboard.Model.Dtos;

namespace MyDashboard.Service.Interface
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(LoginDto user);
        Task<TokenDto> RefreshTokenAsync(TokenDto token);
        Task<bool> RegisterAsync(RegisterDto user);
    }
}
