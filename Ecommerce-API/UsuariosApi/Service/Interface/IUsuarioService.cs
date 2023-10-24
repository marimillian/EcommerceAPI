using FluentResults;
using UsuariosApi.Data.DTO;
using UsuariosApi.Data.DTO.UserCliente;
using UsuariosApi.Data.DTO.Usuario;
using UsuariosApi.Request;

namespace UsuariosApi.Service.Interface;

public interface IUsuarioService
{
    public Task<Result> CadastrarLojista(CreateLojistaDto lojistaDto);
    public Task<Result> CadastrarCliente(CreateClienteDto clienteDto);
    public Task<List<ReadUsuariosDto>> PesquisarUsuarios(FilterUsuariosDto filtro);
    public Task<Result> EditarUsuario(UpdateUsuarioDto usuarioDto, int id);
    public Task<Result> DeletarUsuario(int id);
    public Task<Result> ConfirmacaoEmail(ConfirmacaoEmailRequest request);
    public Task<Result> AlteracaoPermissao(AlteracaoPermissaoRequest request);
    public Task<Result> AlteracaoStatus(AlteracaoStatusRequest request);
}
