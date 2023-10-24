using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.CentroDistribuicao;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_API.Models;
using Ecommerce_API.Data.DTOS.Links;
using Ecommerce_API.Response.CentroDistribuicao;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Ecommerce_API.Controllers;

[ApiController]
[Route("[controller]")]
public class CentroDistribuicaoController : ControllerBase
{
    private EcommerceContext _context;
    private ICentroDistribuicaoService _service;
    private IMapper _mapper;

    public CentroDistribuicaoController(EcommerceContext context, ICentroDistribuicaoService service, IMapper mapper)
    {
        _context = context;
        _service = service;
        _mapper = mapper;
    }

    [HttpPost(Name = "CadastroCentro")]
    //[Authorize(Roles = "admin, lojista")]

    public async Task<IActionResult> CadastrarCentroDistribuicao([FromBody] CreateCentroDistribuicaoDto centroDto)
    {
        var centro = new CentroDistribuicao();
        centro = await _service.CadastrarCentroDistribuicao(centroDto);

        ReadCentroDistribuicaoDto readCentro = _mapper.Map<ReadCentroDistribuicaoDto>(centro);
        var links = CriacaoLinks(readCentro);

        CentroDistribuicaoResponse response = new CentroDistribuicaoResponse(readCentro, links);
        return CreatedAtAction(nameof(PesquisarCentroDistribuicaoId),
                 new { id = centro.Id }, response);
    }

    [HttpGet]

    public IActionResult PesquisarCentroDistribuicao([FromBody] FilterCentroDistribuicaoDto filtro)
    {
        var centro = _service.PesquisarCentroDistribuicao(filtro);
        if (centro == null) return NotFound();
        return Ok(centro);
    }

    [HttpGet("{id}", Name = "PesquisarCentro")]
    [Authorize(Roles = "admin, lojista")]
    public IActionResult PesquisarCentroDistribuicaoId(int id)
    {
        var centro = _service.PesquisarCentroDistribuicaoId(id);
        if (centro == null) return NotFound();

        var links = CriacaoLinks(centro);

        CentroDistribuicaoResponse response = new CentroDistribuicaoResponse(centro, links);

        return Ok(response);
    }

    [HttpPut("{id}", Name = "EditarCentro")]
    //[Authorize(Roles = "admin, lojista")]

    public async Task<IActionResult> EditarCentroDistribuicao([FromBody] UpdateCentroDistribuicaoDto centroDto, int id)
    {
        var centro = await _service.EditarCentroDistribuicao(centroDto, id);
        if (centro == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeletarCentro")]
    //[Authorize(Roles = "admin, lojista")]

    public async Task<IActionResult> ApagarCentroDistribuicao(int id)
    {
        var centro = await _service.ApagarCentroDistribuicao(id);
        if (centro == null) return NotFound();
        return NoContent();

    }

    private List<LinksDto> CriacaoLinks(ReadCentroDistribuicaoDto centro)
    {
        var links = new List<LinksDto>();

        links.Add(
            new LinksDto(Url.Link("DeletarCentro", new { centro.Id }),
            "deletar_centro",
            "DELETE"));
        links.Add(
            new LinksDto(Url.Link("EditarCentro", new { centro.Id }),
            "editar_centro",
            "PUT"));

        links.Add(
           new LinksDto(Url.Link("PesquisarCentro", new { centro.Id }),
           "pesquisar_centro",
           "GET"));

        return links;
    }
}
