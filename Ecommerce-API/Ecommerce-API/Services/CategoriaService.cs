using AutoMapper;
using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Data;
using Ecommerce_API.Models;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_API.Services.Interfaces;
using Ecommerce_API.Repository.Interfaces;


namespace Ecommerce_API.Services;

public class CategoriaService : ICategoriaService
{
    private EcommerceContext _context;
    private IMapper _mapper;
    private ICategoriaRepository _repository;
    //private ILogger<CategoriaService> _logger; 

    public CategoriaService(EcommerceContext context, IMapper mapper, ICategoriaRepository repository) //ILogger<CategoriaService> logger)
    {
        _context = context;
        _mapper = mapper;
        _repository = repository;
        //_logger = logger;
    }

    private List<Categoria> ListaCategoria()
    {
        return _context.Categorias.ToList();
    }

   
    public async Task<Categoria> CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        //_logger.LogInformation("Foi requisitada as regras de negócio para cadastrar uma Categoria");
        Categoria categoria = _mapper.Map<Categoria>(categoriaDto);                      
        VerificacaoDosDados(categoriaDto.Nome);
        await _repository.CadastrarCategoria(categoria);
        return categoria;

    }

    public List<ReadCategoriaDto> PesquisarCategoria(FilterCategoriaDto filtro)
    {
        //_logger.LogInformation($"Foi requisitada as regras de negócio para pesquisar uma Categoria");
        var categoria = _repository.PesquisarCategoria(filtro);
        if (categoria == null) return null;

        var categoriaDto = new List<ReadCategoriaDto>();
        _mapper.Map(categoria, categoriaDto);
      
        return categoriaDto;

    }
      
    public ReadCategoriaDto PesquisarCategoriaId(int id)
    {
       // _logger.LogInformation($"Foi requisitada as regras de negócio para pesquisar a Categoria de ID: {id}");
        var categoria = _repository.PesquisarCategoriaId(id);
        if (categoria == null) return null;
        var categoriaDto = _mapper.Map<ReadCategoriaDto>(categoria);
        return categoriaDto;

    }

    public async Task<Categoria> EditarCategoria(int id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        //_logger.LogInformation($"Foi requisitada as regras de negócio para editar uma Categoria de ID: {id}");
        var categoria = _repository.BuscarPorId(id);
        //Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
        if (categoria == null) return null;
        VerificacaoDosDados(categoriaDto.Nome, id);
        AlteracaoStatus(id, categoriaDto);
        _mapper.Map(categoriaDto, categoria);
        await _repository.EditarCategoria(categoria);


        return categoria;
    }

    public async Task<Categoria> ApagarCategoria(int id)
    {
        //_logger.LogInformation($"Foi requisitada as regras de negócio para deletar uma Categoria de ID: {id}");
        var categoria = await _repository.ApagarCategoria(id);
        if (categoria == null) return null;
        return categoria;
    }

   
    private void AlteracaoStatus(int id, UpdateCategoriaDto categoriaDto)
    {
        if (categoriaDto.Status == false)
        {

            foreach (var sub in _context.SubCategorias)
            {
                if (sub.CategoriaId == id)
                {
                    sub.Status = false;
                    sub.DataModificacao = DateTime.Now;
                   // _logger.LogInformation($"Foi realizada as alterações de status da Subcategoria de ID: {sub.Id}");
                }

            }

            foreach (var prod in _context.Produtos)
            {
                if (prod.CategoriaId == id)
                {
                    prod.Status = false;
                    prod.DataModificacao = DateTime.Now;
                    //_logger.LogInformation($"Foi realizada as alterações de status do Produto de ID: {prod.Id}");
                }
            }

        }

        if (categoriaDto.Status == true)
        {
            foreach (var sub in _context.SubCategorias)
            {
                if (sub.CategoriaId == id)
                {
                    sub.Status = true;
                    sub.DataModificacao = DateTime.Now;
                   // _logger.LogInformation($"Foi realizada as alterações de status da Subcategoria de ID: {sub.Id}");
                }
            }

            foreach (var prod in _context.Produtos)
            {
                if (prod.CategoriaId == id)
                {
                    prod.Status = true;
                    prod.DataModificacao = DateTime.Now;
                   // _logger.LogInformation($"Foi realizada as alterações de status do Produto de ID: {prod.Id}");
                }
            }
        }
    }

   
    private void VerificacaoDosDados(string nome, int? id = null)
    {
        if (ListaCategoria().Any(c => c.Nome.ToUpper() == nome.ToUpper() && c.Id != id))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.Categoria.ErrorMessage.categoriaExistente);
        }

        if (_context.SubCategorias.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.Categoria.ErrorMessage.subcategoriaExistente);
        }

        if (_context.Produtos.Any(p => p.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.Categoria.ErrorMessage.produtoExistente);
        }

        if (_context.CentrosDistribuicoes.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.Categoria.ErrorMessage.centroExistente);
        }
    }
   
}
