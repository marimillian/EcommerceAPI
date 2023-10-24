using AutoMapper;
using Ecommerce_API.Data.DTOS.Links;
using Ecommerce_API.Data.DTOS.SubCategoria;
using Ecommerce_API.Models;
using Ecommerce_API.Response.Subcategoria;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers;

[ApiController]
[Route("[controller]")]
public class SubCategoriaController : ControllerBase
{
    private ISubcategoriaService _service;
    private ILogger<SubCategoriaController> _logger;
    private IMapper _mapper;
    public SubCategoriaController(ISubcategoriaService service, ILogger<SubCategoriaController> logger, IMapper mapper)
    {
        _service = service;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost]
    //[Authorize(Roles = "admin, lojista")]

    public async Task<IActionResult> CadastrarSubCategoria([FromBody] CreateSubCategoriaDto subCategoriaDto)
    {
        _logger.LogInformation("Foi requisitada a inserção de uma nova Subcategoria");
        var subCategoria = new SubCategoria();
        subCategoria = await _service.CadastrarSubCategoria(subCategoriaDto);

        ReadSubCategoriaDto readSubcategoria = _mapper.Map<ReadSubCategoriaDto>(subCategoria);

        var links = CriacaoLinks(readSubcategoria);

        SubcategoriaResponse response = new SubcategoriaResponse(readSubcategoria, links);

        return CreatedAtAction(nameof(PesquisarSubCategoria),
            new { id = subCategoria.Id }, response);

    }

    [HttpGet]
    public IActionResult PesquisarSubCategoria([FromQuery] FilterSubCategoriaDto filtro)
    {
        _logger.LogInformation("Foi requisitada a pesquisa de uma Subcategoria");
        var subCategoria = _service.PesquisarSubcategoria(filtro);
        if (subCategoria == null)
        {
            _logger.LogError("Ocorreu um erro. A Subcategoria não foi localizada");
            return NotFound();
        }
        return Ok(subCategoria);

    }


    [HttpGet("{id}", Name = "PesquisarSubcategoria")]

    public IActionResult PesquisarSubCategoriaId(int id)
    {
        _logger.LogInformation($"Foi requisitada a pesquisa de uma Subcategoria de ID: {id}");
        var subCategoria = _service.PesquisarSubCategoriaId(id);
        if (subCategoria == null)
        {
            _logger.LogError($"Ocorreu um erro. A Subcategoria de ID: {id} não foi localizada");
            return NotFound();
        }
        var links = CriacaoLinks(subCategoria);
        SubcategoriaResponse response = new SubcategoriaResponse(subCategoria, links);
        return Ok(response);
    }

    [HttpPut("{id}", Name = "EditarSubcategoria")]
    //[Authorize(Roles = "admin, lojista")]

    public async Task<IActionResult> EditarSubCategoria([FromBody] UpdateSubCategoriaDto subCategoriaDto, int id)
    {
        _logger.LogInformation($"Foi requisitada a edição de uma Subcategoria de ID: {id}");
        var subCategoria = new SubCategoria();
        if (subCategoria == null)
        {
            _logger.LogError($"Ocorreu um erro. A Subcategoria de ID: {id} não foi localizada");
            return NotFound();
        }

        subCategoria = await _service.EditarSubCategoria(subCategoriaDto, id);

        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeletarSubcategoria")]
    //[Authorize(Roles = "admin, lojista")]
    public async Task<IActionResult> ApagarSubCategoria(int id)
    {
        _logger.LogInformation($"Requisição para deletar uma Subcategoria de ID: {id}");
        var subCategoria = await _service.ApagarSubCategoria(id);
        if (subCategoria == null)
        {
            _logger.LogError($"Ocorreu um erro. A Subcategoria de ID: {id} não foi localizada");
            return NotFound();
        }
        return NoContent();

    }

    private List<LinksDto> CriacaoLinks(ReadSubCategoriaDto subcategoria)
    {
        var links = new List<LinksDto>();

        links.Add(
            new LinksDto(Url.Link("DeletarSubcategoria", new { subcategoria.Id }),
            "deletar_subcategoria",
            "DELETE"));
        links.Add(
            new LinksDto(Url.Link("EditarSubcategoria", new { subcategoria.Id }),
            "editar_subcategoria",
            "PUT"));

        links.Add(
           new LinksDto(Url.Link("PesquisarSubcategoria", new { subcategoria.Id }),
           "pesquisar_subcategoria",
           "GET"));

        return links;
    }

}
