using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyDashboard.Model;
using MyDashboard.Model.Dtos;
using MyDashboard.Model.Entities.Entities;
using MyDashboard.Repository;
using MyDashboard.Repository.Interface;
using MyDashboard.Service.Interface;

namespace MyDashboard.Service;

public class AuthService: IAuthService
{
    private readonly IAuthRepositoy _authRepositoy;
    private readonly MyDashboardSettings _appSettings;
    private readonly IMapper _mapper;

    public AuthService(IAuthRepositoy authRepositoy, 
        IOptions<MyDashboardSettings> options,
        IMapper mapper)
    {
        _authRepositoy = authRepositoy;
        _appSettings = options.Value;
        _mapper = mapper;
    }

    public async Task<TokenDto> LoginAsync(LoginDto user)
    {
        var userExist = _mapper.Map<UserDto>(await _authRepositoy
            .ValidateUserAsync(_mapper.Map<User>(user)));

        if (userExist == null || !AuthHelper.VerifyPassword(user.Username, user.Password, userExist.PasswordHash))
            throw new UnauthorizedAccessException();

        var accessToken = AuthHelper.GenerateAccessToken(userExist, _appSettings.Jwt);
        var refreshToken = AuthHelper.GenerateRefreshToken(userExist);

        await SaveRefreshTokenAsync(refreshToken, userExist.Id);

        return new TokenDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<TokenDto> RefreshTokenAsync(TokenDto token)
    {
        var principal = AuthHelper.GetPrincipalFromExpiredToken(token.AccessToken, _appSettings.Jwt);

        var userExist = _mapper.Map<UserDto>(await _authRepositoy
            .GetUserOfRefreshToken(token.RefreshToken));

        if (userExist == null)
            throw new UnauthorizedAccessException();

        var newAccessToken = AuthHelper.GenerateAccessToken(userExist, _appSettings.Jwt);
        var newRefreshToken = AuthHelper.GenerateRefreshToken(userExist);

        await SaveRefreshTokenAsync(newRefreshToken, userExist.Id);

        return new TokenDto(){
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    public async Task<bool> RegisterAsync(RegisterDto user)
    {
        user.Password = AuthHelper
                    .HashPassword(user.Username, user.Password);

        return await _authRepositoy
            .RegisterAsync(_mapper.Map<User>(user));
    }

    #region 

    private async Task SaveRefreshTokenAsync(string token, int userId)
    {
        var refresh = new RefreshTokenDto()
        {
            Token = token,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(_appSettings.Jwt.RefreshTokenExpirationDays),
            RevokedAt = DateTime.UtcNow.AddDays(_appSettings.Jwt.RefreshTokenExpirationDays),
            UserId = userId,
        };

        await _authRepositoy.SaveRefreshTokenAsync(_mapper.Map<RefreshToken>(refresh));
    }

    #endregion
}
