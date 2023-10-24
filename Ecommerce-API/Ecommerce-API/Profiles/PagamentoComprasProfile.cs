using AutoMapper;
using Ecommerce_API.Data.DTOS.FinalizacaoCompra;
using Ecommerce_API.Data.DTOS.PagamentoDaCompra;
using Ecommerce_API.Models;

namespace Ecommerce_API.Profiles;

public class PagamentoComprasProfile : Profile
{
    public PagamentoComprasProfile()
    {
        CreateMap<CartaoCreditoDto, ReadCartaoCredito>();
        CreateMap<CarrinhoDeComprasModel, ReadCartaoCredito>()
            .ForMember(c => c.CarrinhoId, opts => opts.MapFrom(c => c.Id));
        CreateMap<CartaoDebitoDto, ReadCartaoDebito>();
        CreateMap<CarrinhoDeComprasModel, ReadCartaoDebito>()
            .ForMember(c => c.CarrinhoId, opts => opts.MapFrom(c => c.Id));
        CreateMap<CarrinhoDeComprasModel, ReadPix>()
            .ForMember(c => c.CarrinhoId, opts => opts.MapFrom(c => c.Id)); 
        CreateMap<CarrinhoDeComprasModel, ProdutoNoCarrinhoModel>();
        CreateMap<ProdutoNoCarrinhoModel, Produto>();
    }
}
