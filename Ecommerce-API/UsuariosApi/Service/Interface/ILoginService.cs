using FluentResults;
using UsuariosApi.Request;

namespace UsuariosApi.Service.Interface
{
    public interface ILoginService
    {
        public Task<Result> LogarUsuario (LoginRequest request);
        public Task<Result> SolicitarResetSenha(SolicitarResetSenhaRequest request);
        public Task<Result> ResetSenha(ResetSenhaRequest request);
    }
}
