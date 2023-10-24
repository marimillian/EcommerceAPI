using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.DTO;
using UsuariosApi.Modelo;
using UsuariosApi.Request;

namespace UsuariosApi.Repository.Interface
{
    public interface IUsuarioRepository
    {
        public Task<List<CustomIdentityUser>> PesquisarUsuarios(FilterUsuariosDto filtro);
        public CustomIdentityUser BuscarUsuario(int id);
    }
}
