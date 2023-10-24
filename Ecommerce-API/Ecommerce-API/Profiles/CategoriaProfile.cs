using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Models;
using AutoMapper;
using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Response.Categoria;

namespace Ecommerce_API.Profiles;

public class CategoriaProfile: Profile
{
    public CategoriaProfile()
    {
        CreateMap<CreateCategoriaDto, Categoria>();
        CreateMap<UpdateCategoriaDto, Categoria>();
        CreateMap<Categoria, ReadCategoriaDto>().
            ForMember(categoriaDto => categoriaDto.QtdSubcategorias, opts => opts.MapFrom(categoria => categoria.SubCategorias.Count()))
            .ForMember(categoriaDto => categoriaDto.DataDeCriacao, opts => opts.MapFrom(categoria => categoria.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(categoriaDto => categoriaDto.DataDeModificacao, opts => opts.MapFrom(categoria => categoria.DataModificacao == default
            ? "Não houve alterações."
            : categoria.DataModificacao.ToString("dd/MM/yyyy HH:mm:ss")));
                  

    }
}
