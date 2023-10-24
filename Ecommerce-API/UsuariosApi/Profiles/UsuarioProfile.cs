using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.DTO;
using UsuariosApi.Data.DTO.UserCliente;
using UsuariosApi.Data.DTO.Usuario;
using UsuariosApi.Modelo;

namespace UsuariosApi.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        
        CreateMap<CreateClienteDto, CustomIdentityUser>()
            .ForMember(u => u.UserName, opts => opts.MapFrom(dto => dto.Email))
            .ForMember(usuario => usuario.Perfil, opts => opts.MapFrom(usuarioDto => usuarioDto._perfilUsuario));
        CreateMap<CreateLojistaDto, CustomIdentityUser>()
            .ForMember(u => u.UserName, opts => opts.MapFrom(dto => dto.Email))
            .ForMember(usuario => usuario.Perfil, opts => opts.MapFrom(usuarioDto => usuarioDto._perfilUsuario));

        CreateMap<UpdateUsuarioDto, CustomIdentityUser>()
            .ForMember(u => u.UserName, opts => opts.MapFrom(dto => dto.Email));

        CreateMap<EnderecoModel, CreateLojistaDto>();
        CreateMap<EnderecoModel, CreateClienteDto>();
        CreateMap<EnderecoModel, UpdateUsuarioDto>();
        
        CreateMap<CustomIdentityUser, ReadUsuariosDto>()
            .ForMember(usuarioDto => usuarioDto.DataDeNascimento, 
            opts => opts.MapFrom(usuario => usuario.DataNascimento.ToString("dd/MM/yyyy")))
            .ForMember(usuarioDto => usuarioDto.DataDeCadastro, 
            opts => opts.MapFrom(usuario => usuario.DataCadastro.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(usuarioDto => usuarioDto.DataDeAtualizacao, 
            opts => opts.MapFrom(usuario => usuario.DataAtualizacao == default
            ? "Não houve alterações."
            : usuario.DataAtualizacao.ToString("dd/MM/yyyy HH:mm:ss"))); 
    }
}
