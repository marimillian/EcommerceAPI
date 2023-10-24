using AutoMapper;
using Ecommerce_API.Data.DTOS.SubCategoria;
using Ecommerce_API.Models;

namespace Ecommerce_API.Profiles;

public class SubCategoriaProfile : Profile
{
    public SubCategoriaProfile()
    {

        CreateMap<CreateSubCategoriaDto,  SubCategoria>();
        CreateMap<UpdateSubCategoriaDto, SubCategoria>();
        CreateMap<SubCategoria, ReadSubCategoriaDto>()
            .ForMember(subCategoriaDto => subCategoriaDto.CategoriaNome, 
            opts => opts.MapFrom(subCategoria => subCategoria.Categoria.Nome))
            .ForMember(subCategoriaDto => subCategoriaDto.QtdProdutos, opts => opts.MapFrom(subCategoria => subCategoria.Produtos.Count()))
            .ForMember(subcategoriaDto => subcategoriaDto.DataDeCriacao, opts => opts.MapFrom(subcategoria => subcategoria.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(subcategoriaDto => subcategoriaDto.DataDeModificacao, opts => opts.MapFrom(subcategoria => subcategoria.DataModificacao == default
            ? "Não houve alterações."
            : subcategoria.DataModificacao.ToString("dd/MM/yyyy HH:mm:ss"))); ;

    }
}
