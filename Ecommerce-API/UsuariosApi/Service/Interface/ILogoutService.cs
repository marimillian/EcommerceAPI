using FluentResults;

namespace UsuariosApi.Service.Interface
{
    public interface ILogoutService
    {
        public Task<Result> DeslogarUsuario();
    }
}
