using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.DTO;
using UsuariosApi.Data.DTO.UserCliente;
using UsuariosApi.Data.DTO.Usuario;
using UsuariosApi.Request;
using UsuariosApi.Service.Interface;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[controller]")]

public class UsuarioController : ControllerBase
{
    private IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    
    [HttpPost("/usuariolojista")]
    [Authorize(Roles = "lojista, admin")]
    public async Task<IActionResult> CadastrarUsuarioLojista(CreateLojistaDto lojistaDto)
    {
        Result result = await _usuarioService.CadastrarLojista(lojistaDto);
        if (result.IsFailed)
        {
            return StatusCode(500);
        }
        return Ok($"Cadastro realizado com sucesso! Token de validação: {result.Successes.FirstOrDefault()}");
    }

    
    [HttpPost("/usuariocliente")]
    public async Task<IActionResult> CadastrarUsuarioCliente(CreateClienteDto clienteDto)
    {
        Result result = await _usuarioService.CadastrarCliente(clienteDto);
        if (result.IsFailed)
        {
            return StatusCode(500);
        }
        return Ok($"Cadastro realizado com sucesso! Token de validação: {result.Successes.FirstOrDefault()}");
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> PesquisarUsuarios([FromQuery] FilterUsuariosDto filtro)
    {
        var usuarios = await _usuarioService.PesquisarUsuarios(filtro);
        return Ok(usuarios);
    }


    [HttpPut("{id}")]

    public async Task<IActionResult> EditarUsuario(UpdateUsuarioDto usuarioDto, int id)
    {
        Result result = await _usuarioService.EditarUsuario(usuarioDto, id);
        if (result.IsFailed)
        {
            return StatusCode(500);
        }
        return Ok("Usuário editado com sucesso!");
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeletarUsuario(int id)
    {
        Result result = await _usuarioService.DeletarUsuario(id);
        if (result.IsFailed)
        {
            return StatusCode(500);
        }
        return Ok("Usuário deletado com sucesso!");
    }

    [HttpPost("/confirmacao")]

    public async Task<IActionResult> ConfirmacaoEmail(ConfirmacaoEmailRequest request)
    {
        Result result = await _usuarioService.ConfirmacaoEmail(request);
        if (result.IsFailed)
        {
            return StatusCode(500);
        }
        return Ok(result.Successes.FirstOrDefault());
    }

    [HttpPut("/alteracaopermissao")]
    [Authorize(Roles = "admin")]

    public async Task<IActionResult> AlteracaoPermissao(AlteracaoPermissaoRequest request)
    {
        Result result = await _usuarioService.AlteracaoPermissao(request);
        if (result.IsFailed)
        {
            return StatusCode(500);
        }
        return Ok(result.Successes.FirstOrDefault());
    }

    [HttpPut("/alteracaostatus")]
    [Authorize(Roles = "admin")]

    public async Task<IActionResult> AlteracaoPermissao(AlteracaoStatusRequest request)
    {
        Result result = await _usuarioService.AlteracaoStatus(request);
        if (result.IsFailed)
        {
            return StatusCode(500);
        }
        return Ok(result.Successes.FirstOrDefault());
    }
}
