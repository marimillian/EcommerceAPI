using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Data;
using UsuariosApi.Data.DTO;
using UsuariosApi.Modelo;
using UsuariosApi.Repository.Interface;
using UsuariosApi.Request;

namespace UsuariosApi.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private UserManager<CustomIdentityUser> _userManager;
    private UsuarioContext _context;

    public UsuarioRepository(UserManager<CustomIdentityUser> userManager, UsuarioContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public CustomIdentityUser BuscarUsuario(int id)
    {
        return _userManager.Users.FirstOrDefault(u => u.Id == id);
    }

    public async Task<List<CustomIdentityUser>> PesquisarUsuarios(FilterUsuariosDto filtro)
    {

        var sql = "SELECT * FROM `USUARIODB`.ASPNETUSERS WHERE 1=1";

        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            sql += $" AND LOCATE ('{filtro.Nome}', NOME)";
        }

        if (filtro.Status == false)
        {
            sql += $" AND STATUS = FALSE";
        }

        if (filtro.Status == true)
        {
            sql += $" AND STATUS = TRUE";
        }

        if (!string.IsNullOrEmpty(filtro.CPF))
        {
            sql += $" AND CPF = {filtro.CPF}";
        }

        if (!string.IsNullOrEmpty(filtro.Email))
        {
            sql += $" AND LOCATE ('{filtro.Email}', EMAIL)";
        }

        var usuarios = _context.Users.FromSqlRaw(sql).ToList();
        return usuarios;

    }   

}
