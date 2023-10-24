using Ecommerce_API.Data.DTOS.FinalizacaoCompra;
using Ecommerce_API.Data.DTOS.PagamentoDaCompra;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers;

[ApiController]
[Route("[controller]")]

public class PagamentoDeComprasController : ControllerBase
{
    private IPagamentoCompraService _service;

    public PagamentoDeComprasController(IPagamentoCompraService service)
    {
        _service = service;
    }

    [HttpPost("/PagamentoCredito/Carrinho/{carrinhoId}")]

    public async Task<IActionResult> PagamentoCartaoCredito([FromBody] CartaoCreditoDto cartaoCreditoDto, int carrinhoId)
    {
        var cartaoCredito = new ReadCartaoCredito();
        cartaoCredito = await _service.PagamentoCartãoDeCredito(carrinhoId, cartaoCreditoDto);

        return Ok(cartaoCredito);

    }

    [HttpPost("/PagamentoDebito/Carrinho/{carrinhoId}")]

    public async Task<IActionResult> PagamentoCartaoDebito([FromBody] CartaoDebitoDto cartaoDebitoDto, int carrinhoId)
    {
        var cartaoDebito = new ReadCartaoDebito();
        cartaoDebito = await _service.PagamentoCartaoDeDebito(carrinhoId, cartaoDebitoDto);

        return Ok(cartaoDebito);
    }

    [HttpPost("/PagamentoPix/Carrinho/{carrinhoId}")]

    public async Task<IActionResult> PagamentoPix(int carrinhoId)
    {
        var pix = new ReadPix();
        pix = await _service.PagamentoPix(carrinhoId);

        return Ok(pix);
    }
}
