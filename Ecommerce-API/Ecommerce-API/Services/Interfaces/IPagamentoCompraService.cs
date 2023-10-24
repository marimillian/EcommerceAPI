using Ecommerce_API.Data.DTOS.FinalizacaoCompra;
using Ecommerce_API.Data.DTOS.PagamentoDaCompra;

namespace Ecommerce_API.Services.Interfaces;

public interface IPagamentoCompraService
{
    public Task<ReadCartaoCredito> PagamentoCartãoDeCredito(int carrinhoId, CartaoCreditoDto cartaoCreditoDto);
    public Task<ReadCartaoDebito> PagamentoCartaoDeDebito(int carrinhoId, CartaoDebitoDto cartaoDebitoDto);
    public Task<ReadPix> PagamentoPix(int carrinhoId);
}
