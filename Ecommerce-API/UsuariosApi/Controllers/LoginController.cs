using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Request;
using UsuariosApi.Service.Interface;

namespace UsuariosApi.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost] 

    public async Task<IActionResult> LogarUsuario(LoginRequest request)
    {
        Result result = await _loginService.LogarUsuario(request);
        if (result.IsFailed)
        {
            return Unauthorized($"Não foi possível efetuar o login. Por favor, verifique se os dados estão corretos");

        }

        return Ok(result.Successes.FirstOrDefault());
    }

    [HttpPost("/solicitareset")]

    public async Task<IActionResult> SolicitarResetSenhaUsuario(SolicitarResetSenhaRequest request)
    {
        Result result = await _loginService.SolicitarResetSenha(request);
        if (result.IsFailed) 
        { 
            return Unauthorized(result.Errors.FirstOrDefault()); 
        }
        return Ok(result.Successes.FirstOrDefault());
    }

    [HttpPost("/resetsenha")]

    public async Task<IActionResult> ResetSenhaUsuario(ResetSenhaRequest request)
    {
        Result result = await _loginService.ResetSenha(request);
        if (result.IsFailed)
        {
            return Unauthorized(result.Errors.FirstOrDefault());
        }
        return Ok(result.Successes.FirstOrDefault());
    }
}
