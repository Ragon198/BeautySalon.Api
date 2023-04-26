using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BeautySalon.ContractService;
using BeautySalon.Crosscutting.Exceptions;
using BeautySalon.Crosscutting.Extensions;
using BeautySalon.DTO.Auths;
using BeautySalon.DTO.Users;
using BeautySalon.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BeautySalon.Service;

public class AuthService : BaseService, IAuthService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly AppSettings _appSettings;
    private readonly SymmetricSecurityKey _key;
    private readonly SigningCredentials _credentials;

    public AuthService(
        INotifier notifier, 
        SignInManager<User> signInManager, 
        UserManager<User> userManager,
        IOptions<AppSettings> appSettings) : base(notifier)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
        _credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
    }

    public async Task<TokenDto> Login(UserLoginDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.Pin, dto.Password, false, true);

        if (!result.Succeeded && result.IsLockedOut) throw new BeautySalonApplicationException("Número de tentativas excedido.");
        else if (!result.Succeeded) throw new BeautySalonApplicationException("Pin ou senha incorreto(s).");

        User user = await _userManager
                .Users
                .FirstOrDefaultAsync(userEntity => userEntity.NormalizedUserName.Equals(dto.Pin.ToUpper()));
        return await GenerateToken(user);
    }

    private async Task<TokenDto> GenerateToken(User user)
    {
        if (!user.Active) throw new BeautySalonApplicationException("Usuário inativo.");

        List<Claim> claims = new List<Claim>
        {
            new Claim("pin", user.Pin),
            new Claim("userName", user.UserName),
            new Claim("name", user.Name),
            new Claim("userId", user.Id.ToString())
        };
        var roles = await _userManager.GetRolesAsync(user);
        roles.ToList().ForEach(x => claims.Add(new Claim("roles", x)));

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: _credentials,
            expires: DateTime.UtcNow.AddHours(_appSettings.ExpireHour),
            issuer: _appSettings.Issuer,
            audience: _appSettings.ValidIn
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        if (string.IsNullOrEmpty(tokenString)) throw new BeautySalonApplicationException("Algo de errado ocorreu ao gerar o token de acesso.");

        List<Dictionary<string, string>> claimDictionary = new();
        claims.Where(x => x.Type != "name").ToList().ForEach(claim =>
        {
            Dictionary<string, string> dict = new();
            dict.Add(claim.Type, claim.Value);
            claimDictionary.Add(dict);
        });

        TokenDto tokenResponse = new()
        {
            AccessToken = tokenString,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpireHour).TotalSeconds,
            Claims = claimDictionary
        };
        return tokenResponse;
    }
}
