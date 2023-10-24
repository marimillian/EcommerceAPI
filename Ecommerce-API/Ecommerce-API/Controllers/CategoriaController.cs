using AutoMapper;
using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Data.DTOS.Links;
using Ecommerce_API.Models;
using Ecommerce_API.Response.Categoria;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{    
    private ICategoriaService _service;
    private ILogger<CategoriaController> _logger;
    private IMapper _mapper;

    public CategoriaController(ICategoriaService service, ILogger<CategoriaController> logger, IMapper mapper)
    {
        _service = service;
        _logger = logger;
        _mapper = mapper;
    
    }
        
    [HttpPost(Name = "CadastroCategoria")]
    //[Authorize(Roles = "admin, lojista")]
    public async Task<IActionResult> CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        _logger.LogInformation("Foi requisitada a inserção de uma nova Categoria");
        var categoria = new Categoria();
        categoria = await _service.CadastrarCategoria(categoriaDto);
        
        ReadCategoriaDto readCategoria = _mapper.Map<ReadCategoriaDto>(categoria);

        var links = CriacaoLinks(readCategoria);
        CategoriaResponse response = new CategoriaResponse(readCategoria, links);

        return CreatedAtAction(nameof(PesquisarCategoriaId),
            new { id = categoria.Id }, response);
       
    }

    [HttpGet]
    public ActionResult PesquisarCategoria([FromQuery] FilterCategoriaDto filtro)
    {
        _logger.LogInformation("Foi requisitada a pesquisa de uma Categoria");
        var categoria = _service.PesquisarCategoria(filtro);
        if (categoria == null)
        {
            _logger.LogError("Ocorreu um erro. A Categoria não foi localizada");
            return NotFound();
        }

               
        return Ok(categoria);
    }

    [HttpGet("{id}", Name = "PesquisarCategoria")]
    public IActionResult PesquisarCategoriaId(int id)
    {
        _logger.LogInformation($"Foi requisitada a pesquisa de uma Categoria de ID: {id}");
        var categoria = _service.PesquisarCategoriaId(id);
        if (categoria == null)
        {
            _logger.LogError($"Ocorreu um erro. Não foi localizada a Categoria de ID: {id}");
            return NotFound(); 
        }
                
        var links = CriacaoLinks(categoria);
        CategoriaResponse response = new CategoriaResponse(categoria, links);
                  
        return Ok(response);
        
                       
    }
      
    [HttpPut("{id}", Name = "EditarCategoria")]
   // [Authorize(Roles = "admin, lojista")]
    public async Task<IActionResult> EditarCategoria (int id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        _logger.LogInformation($"Foi requisitada a edição de uma Categoria de ID: {id}");
        await _service.EditarCategoria(id, categoriaDto);       
        return NoContent(); 
    }

    [HttpDelete("{id}", Name = "DeletarCategoria")]
    [Authorize(Roles = "admin, lojista")]

    public async Task<IActionResult> ApagarCategoria (int id)
    {
        _logger.LogInformation($"Requisição para deletar uma Categoria de ID: {id}");
        var categoria = await _service.ApagarCategoria(id);
        if (categoria == null)
        {
            _logger.LogError($"Ocorreu um erro. Não foi localizada a Categoria de ID: {id}");
            return NotFound();           
        }

        return NoContent();
    }

    private List<LinksDto> CriacaoLinks(ReadCategoriaDto categoria)
    {
        var links = new List<LinksDto>();

        links.Add(
            new LinksDto(Url.Link("DeletarCategoria", new { categoria.Id }),
            "deletar_categoria",
            "DELETE"));
        links.Add(
            new LinksDto(Url.Link("EditarCategoria", new { categoria.Id }),
            "editar_categoria",
            "PUT"));

        links.Add(
           new LinksDto(Url.Link("PesquisarCategoria", new { categoria.Id }),
           "pesquisar_categoria",
           "GET"));

        return links;
    }
}
