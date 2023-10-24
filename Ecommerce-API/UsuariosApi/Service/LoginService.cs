using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Modelo;
using UsuariosApi.Request;
using UsuariosApi.Service.Interface;

namespace UsuariosApi.Service;

public class LoginService : ILoginService
{
    private SignInManager<CustomIdentityUser> _signInManager;
    private ITokenService _tokenService;

    public LoginService(SignInManager<CustomIdentityUser> signInManager, ITokenService tokenService)
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<Result> LogarUsuario(LoginRequest request)
    {
        var identityUserEmail = _signInManager
            .UserManager
            .FindByEmailAsync(request.Email).Result;
        
        if(identityUserEmail.Status == false)
        {
            throw new UsuarioExceptions("O usuário está inativo, por este motivo, não é possível logar");
        }

        var result = _signInManager
            .PasswordSignInAsync(identityUserEmail.UserName, request.Password, false, false);

        if (result.Result.Succeeded)
        {           
            TokenModel token = await _tokenService
                .CriacaoToken(identityUserEmail, _signInManager
                .UserManager
                .GetRolesAsync(identityUserEmail).Result.FirstOrDefault());

            return Result.Ok().WithSuccess(token.Value);
        }
        throw new UsuarioExceptions("Login falhou.");
    }

    public async Task<Result> SolicitarResetSenha(SolicitarResetSenhaRequest request)
    {
        CustomIdentityUser identityUser = await RecuperaUsuarioPorEmail(request.Email);

        if (identityUser != null)
        {
            string codigoRecuperacao = _signInManager
                .UserManager
                .GeneratePasswordResetTokenAsync(identityUser).Result;
            return Result.Ok().WithSuccess(codigoRecuperacao);
        }

        throw new UsuarioExceptions("Houve uma falha ao resetar a senha");
    }


    public async Task<Result> ResetSenha(ResetSenhaRequest request)
    {
        CustomIdentityUser identityUser = await RecuperaUsuarioPorEmail(request.Email);

        IdentityResult resultIdentity = _signInManager
            .UserManager
            .ResetPasswordAsync(identityUser, request.Token, request.Password).Result;

        if (resultIdentity.Succeeded)
        {
            return Result.Ok().WithSuccess("Senha redefinida com sucesso!");
        }
        throw new UsuarioExceptions("Não foi possível redefinir a senha.");
    }

    private async Task<CustomIdentityUser> RecuperaUsuarioPorEmail(string email)
    {
        return _signInManager
            .UserManager
            .Users
            .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
    }
}
