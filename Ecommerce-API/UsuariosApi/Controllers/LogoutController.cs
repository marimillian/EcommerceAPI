using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Service.Interface;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private ILogoutService _logoutService;

        public LogoutController(ILogoutService logoutService)
        {
            _logoutService = logoutService;
        }


        [HttpPost]

        public async Task<IActionResult> DeslogarUsuario()
        {
            Result result = await _logoutService.DeslogarUsuario();
            if (result.IsSuccess)
            {
                return Ok(result.Successes);
            }
            return Unauthorized("Não foi possível deslogar");
    }
    }

    
}
