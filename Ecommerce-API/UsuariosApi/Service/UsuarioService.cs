using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using UsuariosApi.Data.DTO;
using UsuariosApi.Data.DTO.UserCliente;
using UsuariosApi.Data.DTO.Usuario;
using UsuariosApi.Modelo;
using UsuariosApi.Repository.Interface;
using UsuariosApi.Request;
using UsuariosApi.Service.Interface;
using UsuariosApi.Service.ValidaçãoCPF;

namespace UsuariosApi.Service
{
    public class UsuarioService : IUsuarioService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private IUsuarioRepository _usuarioRepository;


        public UsuarioService(IMapper mapper, UserManager<CustomIdentityUser> userManager,
            IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;

        }

        public async Task<EnderecoModel> ConsultarViaCep(string cep)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
                var jsonObject = JsonSerializer.Deserialize<EnderecoModel>(response);
                return jsonObject;
            }

            catch
            {
                throw new UsuarioExceptions("O CEP digitado está incorreto.");
            }

        }

        public async Task<Result> CadastrarLojista(CreateLojistaDto lojistaDto)
        {
            EnderecoModel viaCep = await ConsultarViaCep(lojistaDto.CEP);
            _mapper.Map(viaCep, lojistaDto);
            ValidacaoDados(lojistaDto.CPF, lojistaDto.DataNascimento);
                        
            CustomIdentityUser lojistaIdentity = _mapper.Map<CustomIdentityUser>(lojistaDto);

            lojistaIdentity.DataCadastro = DateTime.Now;
            lojistaIdentity.Status = true;

            var result = await _userManager.CreateAsync(lojistaIdentity, lojistaDto.Password);

            await _userManager.AddToRoleAsync(lojistaIdentity, "lojista");

            if (result.Errors.Any())
            {
                throw new UsuarioExceptions("Ocorreu um erro ao cadastrar o usuário. Por favor, tente novamente");
            }
           
            var code = _userManager.GenerateEmailConfirmationTokenAsync(lojistaIdentity).Result;

            return Result.Ok().WithSuccess(code);
        }

        public async Task<Result> CadastrarCliente(CreateClienteDto clienteDto)
        {
            EnderecoModel viaCep = await ConsultarViaCep(clienteDto.CEP);
            _mapper.Map(viaCep, clienteDto);
            ValidacaoDados(clienteDto.CPF, clienteDto.DataNascimento);
                      
            CustomIdentityUser clienteIdentity = _mapper.Map<CustomIdentityUser>(clienteDto);

            clienteIdentity.DataCadastro = DateTime.Now;
            clienteIdentity.Status = true;

            var result = await _userManager.CreateAsync(clienteIdentity, clienteDto.Password);
            await _userManager.AddToRoleAsync(clienteIdentity, "cliente");

            if (result.Errors.Any())
            {
                throw new UsuarioExceptions("Ocorreu um erro ao cadastrar o usuário. Por favor, tente novamente");
            }
            
            var code = _userManager.GenerateEmailConfirmationTokenAsync(clienteIdentity).Result;

            return Result.Ok().WithSuccess(code);
        }

        public async Task<List<ReadUsuariosDto>> PesquisarUsuarios(FilterUsuariosDto filtro)
        {
            var usuarios = await _usuarioRepository.PesquisarUsuarios(filtro);
            var usarioDto = new List<ReadUsuariosDto>();
            _mapper.Map(usuarios, usarioDto);

            return usarioDto;
        }

        public async Task<Result> EditarUsuario(UpdateUsuarioDto usuarioDto, int id)
        {
            EnderecoModel viaCep = await ConsultarViaCep(usuarioDto.CEP);
            _mapper.Map(viaCep, usuarioDto);

            var buscar = _usuarioRepository.BuscarUsuario(id);
            _mapper.Map(usuarioDto, buscar);
            buscar.DataAtualizacao = DateTime.Now;
            var result = await _userManager.UpdateAsync(buscar);

            if (result.Errors.Any())
            {
                throw new UsuarioExceptions("Ocorreu um erro ao editar o usuário. Por favor, tente novamente");                
            }

            return Result.Ok().WithSuccess("Usuário editado com sucesso!");

        }

        public async Task<Result> DeletarUsuario(int id)
        {
            var usuario = _usuarioRepository.BuscarUsuario(id);
            var result = await _userManager.DeleteAsync(usuario);

            if (result.Errors.Any())
            {
                throw new UsuarioExceptions("Ocorreu um erro ao editar o usuário. Por favor, tente novamente");
            }

            return Result.Ok().WithSuccess("Usuario exluído com sucesso!");
        }

        public async Task<Result> ConfirmacaoEmail(ConfirmacaoEmailRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Email == request.Email);
            var identityResult = _userManager.ConfirmEmailAsync(identityUser, request.CodigoAtivacao).Result;

            if (identityResult.Errors.Any())
            {
                throw new UsuarioExceptions("Ocorreu um erro ao ativar a conta do usuário");
                
            }

            return Result.Ok();
        }

        public async Task<Result> AlteracaoPermissao (AlteracaoPermissaoRequest request)
        {
            if (_userManager.Users.Any(u => u.Id == request.Id && u.Perfil == "CLIENTE"))
            {
                throw new UsuarioExceptions("Não é possível alterar a permissão deste usuário");
            }

            var customIdentity = _usuarioRepository.BuscarUsuario(request.Id);
            var role = _userManager.GetRolesAsync(customIdentity).Result.FirstOrDefault();

            var remover = _userManager.RemoveFromRoleAsync(customIdentity, role).Result;

            var result = await _userManager.AddToRoleAsync(customIdentity, request.Role);

            customIdentity.Perfil = request.Role.ToUpper();
            customIdentity.DataAtualizacao = DateTime.Now;

            if (result.Errors.Any())
            {
                throw new UsuarioExceptions("Ocorreu um erro ao editar o usuário. Por favor, tente novamente");
            }            

            return Result.Ok().WithSuccess($"Usuário editado com sucesso! O usuário {customIdentity.UserName}" +
                $" teve a sua permissão alterada para: {request.Role}");
        }

        public async Task<Result> AlteracaoStatus (AlteracaoStatusRequest request)
        {
            var customIdentity = _usuarioRepository.BuscarUsuario(request.Id);
            customIdentity.Status = request.Status;
            customIdentity.DataAtualizacao = DateTime.Now;

            var result = await _userManager.UpdateAsync(customIdentity);

            if (result.ToResult().IsFailed)
            {
                throw new UsuarioExceptions("Ocorreu um erro ao editar o usuário. Por favor, tente novamente");
            }

            return Result.Ok().WithSuccess($"Usuário editado com sucesso! O usuário {customIdentity.UserName} " +
                $"teve o status alterado para: {request.Status}");
        }


        private void ValidacaoDados(string? cpf, DateTime dataNascimento)
        {
            if (ValidacaoCPF.IsCpf(cpf) == false)
            {
                throw new UsuarioExceptions("O CPF está inválido.");
            }

            if (dataNascimento >= DateTime.Today)
            {
                throw new UsuarioExceptions("A data de nascimento está incorreta.");
            }

            if (_userManager.Users.Any(u => u.CPF == cpf))
            {
                throw new UsuarioExceptions("O CPF já foi cadastrado.");
            }

        }

      
    }

   
}
