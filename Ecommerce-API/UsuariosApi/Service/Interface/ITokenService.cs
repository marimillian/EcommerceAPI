using Microsoft.AspNetCore.Identity;
using UsuariosApi.Modelo;

namespace UsuariosApi.Service.Interface
{
    public interface ITokenService
    {
        public Task<TokenModel> CriacaoToken(CustomIdentityUser usuario, string role);
    }
}
