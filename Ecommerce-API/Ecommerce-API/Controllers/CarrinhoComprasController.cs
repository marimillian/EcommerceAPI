using AutoMapper;
using Ecommerce_API.Data.DTOS.CarrinhoDeCompras;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers;

[ApiController]
[Route("[controller]")]

public class CarrinhoComprasController : ControllerBase
{
    private ICarrinhoComprasService _service;
    IMapper _mapper;

    public CarrinhoComprasController(ICarrinhoComprasService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
     
    [HttpPost("/Carrinho/criarCarrinho")]
    public async Task<IActionResult> CriarCarrinho([FromBody] CreateCarrinhoDto carrinhoDto)
    {
        var carrinho = new ReadCarrinhoDto();
        carrinho = await _service.CriarCarrinho(carrinhoDto);       
       
        return CreatedAtAction(nameof(PesquisarCarrinhoId), new { id = carrinho.CarrinhoId }, carrinho);

    }

    [HttpPost("/adicionarProduto/Carrinho/{carrinhoId}/Produto/{produtoId}")]
    public async Task<IActionResult> AdicionarProdutoCarrinho([FromBody] AddProdutoNoCarrinhoDto carrinhoDto, int carrinhoId,
        int produtoId)
    {
        var carrinho = new ReadCarrinhoDto();
        carrinho = await _service.AdicionarProdutoCarrinho(carrinhoDto, carrinhoId, produtoId);

        return Ok(carrinho);
    }

    [HttpPut("/alterarProduto/Carrinho/{carrinhoId}/Produto/{produtoId}")]
    public async Task<IActionResult> AlterarProdutoNoCarrinho([FromBody] UpdateProdutoNoCarrinhoDto carrinhoDto, int carrinhoId,
        int produtoId)
    {
        var carrinho = new ReadCarrinhoDto();
        carrinho = await _service.AlterarProdutoNoCarrinho(carrinhoDto, carrinhoId, produtoId);
        return Ok(carrinho);
    }

    [HttpPost("/endereço/Carrinho/{id}")]

    public async Task<IActionResult> AdicionarEndercoNoCarrinho(int id, [FromBody] AddEndereçoNoCarrinhoDto carrinhoDto)
    {
        var carrinho = new ReadCarrinhoDto();
        carrinho = await _service.AdicionarEndereçoNoCarrinho(id, carrinhoDto);
        return Ok(carrinho);
    }

    [HttpDelete("/Carrinho/{carrinhoId}/Produto/{produtoId}")]

    public async Task<IActionResult> RemoverProdutoDoCarrinho(int carrinhoId, int produtoId)
    {
        var carrinho = new ReadCarrinhoDto();
        carrinho = await _service.RemoverProdutoDoCarrinho(carrinhoId, produtoId);
        return Ok(carrinho);
    }

    [HttpGet]
    public async Task<IActionResult> PesquisarCarrinhoId(int id)
    {
        var carrinho = _service.PesquisarCarrinhoId(id);
        if (carrinho == null)
        {
            return NotFound();
        }

        return Ok(carrinho);
    }

}
