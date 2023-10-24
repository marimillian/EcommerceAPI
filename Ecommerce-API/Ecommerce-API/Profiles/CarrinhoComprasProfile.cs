using AutoMapper;
using Ecommerce_API.Data.DTOS.CarrinhoDeCompras;
using Ecommerce_API.Models;

namespace Ecommerce_API.Profiles;

public class CarrinhoComprasProfile : Profile
{
    public CarrinhoComprasProfile()
    {
        CreateMap<CreateCarrinhoDto, CarrinhoDeComprasModel>();                       
        CreateMap<Endereco, CreateCarrinhoDto>();
        CreateMap<AddProdutoNoCarrinhoDto, CarrinhoDeComprasModel>();
        CreateMap<AddProdutoNoCarrinhoDto, ProdutoNoCarrinhoModel>();
        CreateMap<UpdateProdutoNoCarrinhoDto, ProdutoNoCarrinhoModel>();
        CreateMap<CarrinhoDeComprasModel, ReadCarrinhoDto>()
            .ForMember(c => c.CarrinhoId, opts => opts.MapFrom(c => c.Id));
        CreateMap<CarrinhoDeComprasModel, ProdutoNoCarrinhoModel>()
            .ForMember(c => c.CarrinhoId, opts => opts.MapFrom(c => c.Id));            
        CreateMap<ProdutoNoCarrinhoModel, ReadCarrinhoDto>();
        CreateMap<CreateCarrinhoDto, ProdutoNoCarrinhoModel>()
            .ForMember(c => c.ProdutoId, opts => opts.MapFrom(c => c.ProdutoId));
        CreateMap<Produto, ProdutoNoCarrinhoModel>()
            .ForMember(p => p.NomeProduto, opts => opts.MapFrom(p => p.Nome))
            .ForMember(p => p.ValorProduto, opts => opts.MapFrom(p => p.Valor));
        CreateMap<ProdutoNoCarrinhoModel, ReadProdutoNoCarrinhoDto>();
        CreateMap<Endereco, CarrinhoDeComprasModel>();
        
        
    }
}
