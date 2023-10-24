using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Modelo;
using UsuariosApi.Service.Interface;

namespace UsuariosApi.Service
{
    public class LogoutService : ILogoutService
    {
        private SignInManager<CustomIdentityUser> _signinManager;

        public LogoutService(SignInManager<CustomIdentityUser> signinManager)
        {
            _signinManager = signinManager;
        }

        public async Task<Result> DeslogarUsuario()
        {
            var result = _signinManager.SignOutAsync();
            if (result.IsCompletedSuccessfully) return Result.Ok();
            throw new UsuarioExceptions("Logout falhou.");
        }
    }
}
