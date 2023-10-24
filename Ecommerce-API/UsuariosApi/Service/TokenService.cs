using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Modelo;
using UsuariosApi.Service.Interface;

namespace UsuariosApi.Service;

public class TokenService : ITokenService
{
    IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<TokenModel> CriacaoToken(CustomIdentityUser usuario, string role)
    {
        Claim[] claimUsuario = new Claim[]
        {
            new Claim("username", usuario.UserName),
            new Claim("id", usuario.Id.ToString()),
            new Claim(ClaimTypes.Role, role),
        };

        var chave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetValue<string>("SymmetricSecurityKey")));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claimUsuario,
            signingCredentials: credenciais,
            expires: DateTime.UtcNow.AddHours(1));

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenModel(tokenString);
    }
}
