using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.Links;
using Ecommerce_API.Data.DTOS.Produto;
using Ecommerce_API.Models;
using Ecommerce_API.Response.Produto;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Ecommerce_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private EcommerceContext _context;
        private IProdutoService _service;
        private IMapper _mapper;

        public ProdutoController(EcommerceContext context, IProdutoService service, IMapper mapper)
        {
            _context = context;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        //[Authorize(Roles = "admin, lojista")]
        public async Task<IActionResult> CadastrarProduto([FromBody] CreateProdutoDto produtoDto)
        {
            var produto = new Produto();
            produto = await _service.CadastrarProduto(produtoDto);

            ReadProdutoDto readProduto = _mapper.Map<ReadProdutoDto>(produto);

            var links = CriacaoLinks(readProduto);

            ProdutoResponse response = new ProdutoResponse(readProduto, links);

            return CreatedAtAction(nameof(PesquisarProdutoId),
                new { id = produto.Id }, response);

        }

        [HttpGet]

        public IActionResult PesquisarProduto([FromQuery] FilterProdutoDto filtro)

        {
            var produto = _service.PesquisarProduto(filtro);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpGet("{id}", Name = "PesquisarProduto")]

        public IActionResult PesquisarProdutoId(int id)
        {
            var produto = _service.PesquisarProdutoId(id);
            if (produto == null) return NotFound();
            var links = CriacaoLinks(produto);
            ProdutoResponse response = new ProdutoResponse(produto, links);

            return Ok(response);

        }

        [HttpPut("{id}", Name = "EditarProduto")]
       // [Authorize(Roles = "admin, lojista")]

        public async Task<IActionResult> EditarProduto([FromBody] UpdateProdutoDto produtoDto, int id)
        {
            var produto = await _service.EditarProduto(produtoDto, id);
            if (produto == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeletarProduto")]
       // [Authorize(Roles = "admin, lojista")]

        public async Task<IActionResult> ApagarProduto(int id)
        {
            var produto = await _service.ApagarProduto(id);
            if (produto == null) return NotFound();
            return NoContent();

        }

        private List<LinksDto> CriacaoLinks(ReadProdutoDto produto)
        {
            var links = new List<LinksDto>();

            links.Add(
                new LinksDto(Url.Link("DeletarProduto", new { produto.Id }),
                "deletar_produto",
                "DELETE"));
            links.Add(
                new LinksDto(Url.Link("EditarProduto", new { produto.Id }),
                "editar_produto",
                "PUT"));

            links.Add(
               new LinksDto(Url.Link("PesquisarProduto", new { produto.Id }),
               "pesquisar_produto",
               "GET"));

            return links;
        }

    }
}