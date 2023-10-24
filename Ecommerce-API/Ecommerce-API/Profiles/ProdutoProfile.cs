using Ecommerce_API.Data.DTOS.Produto;
using Ecommerce_API.Models;
using AutoMapper;

namespace Ecommerce_API.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<CreateProdutoDto, Produto>();
        CreateMap<UpdateProdutoDto, Produto>();
        CreateMap<Produto, ReadProdutoDto>()
            .ForMember(produtoDto => produtoDto.CategoriaNome,
            opts => opts.MapFrom(produto => produto.Categoria.Nome))
            .ForMember(produtoDto => produtoDto.SubcategoriaNome,
            opts => opts.MapFrom(produto => produto.SubCategoria.Nome))
            .ForMember(produtoDto => produtoDto.CentroDistribuicao, 
            opts => opts.MapFrom(produto => produto.CentroDistribuicao.Nome))
            .ForMember(produtoDto => produtoDto.DataDeCriacao, opts => opts.MapFrom(produto => produto.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(produtoDto => produtoDto.DataDeModificacao, opts => opts.MapFrom(produto => produto.DataModificacao == default
            ? "Não houve alterações."
            : produto.DataModificacao.ToString("dd/MM/yyyy HH:mm:ss"))); ;




    }
}
